Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading.Tasks
Imports NodeRedDesktop

Namespace Modules

''' <summary>
''' Modulo per la gestione della sicurezza di Node-RED.
''' Gestisce l autenticazione (adminAuth), la restrizione IP e il file settings.js.
''' </summary>
Public Module SecurityManager

#Region "Tipi"

    ''' <summary>
    ''' Rappresenta un utente di Node-RED con credenziali e permessi.
    ''' </summary>
    Public Structure NodeRedUser
        ''' <summary>Nome utente per l accesso all interfaccia admin.</summary>
        Public Username As String
        ''' <summary>Hash bcrypt della password (generato tramite node -e + bcryptjs).</summary>
        Public PasswordHash As String
        ''' <summary>Permessi dell utente: "full" per accesso completo, "read" per sola lettura.</summary>
        Public Permissions As String
    End Structure

#End Region

#Region "Lettura Settings"

    ''' <summary>
    ''' Restituisce il percorso completo del file settings.js di Node-RED.
    ''' </summary>
    Public Function GetSettingsFilePath() As String
        Return Path.Combine(AppSettings.GetUserDir(), "settings.js")
    End Function

    ''' <summary>
    ''' Legge e restituisce il contenuto di settings.js.
    ''' Se il file non esiste, restituisce una stringa vuota.
    ''' </summary>
    Public Function ReadSettingsFile() As String
        Try
            Dim path As String = GetSettingsFilePath()
            If Not File.Exists(path) Then
                LogManager.AddWarn("settings.js non trovato: " & path, "SecurityManager")
                Return String.Empty
            End If
            Return File.ReadAllText(path, Encoding.UTF8)
        Catch ex As Exception
            LogManager.AddError("Errore lettura settings.js: " & ex.Message, "SecurityManager")
            Return String.Empty
        End Try
    End Function

    ''' <summary>
    ''' Crea una copia di backup di settings.js come settings.js.bak nella stessa directory.
    ''' </summary>
    Public Sub BackupSettingsFile()
        Try
            Dim srcPath As String = GetSettingsFilePath()
            If Not File.Exists(srcPath) Then
                LogManager.AddWarn("Backup settings.js: file sorgente non trovato.", "SecurityManager")
                Return
            End If
            Dim bakPath As String = srcPath & ".bak"
            File.Copy(srcPath, bakPath, overwrite:=True)
            LogManager.AddInfo("Backup settings.js creato: " & bakPath, "SecurityManager")
        Catch ex As Exception
            LogManager.AddError("Errore backup settings.js: " & ex.Message, "SecurityManager")
        End Try
    End Sub

    ''' <summary>
    ''' Analizza settings.js e restituisce la lista degli utenti definiti in adminAuth.users.
    ''' Usa regex per estrarre username, password hash e permessi.
    ''' Restituisce una lista vuota se adminAuth non e configurata.
    ''' </summary>
    Public Function ReadUsers() As List(Of NodeRedUser)
        Dim result As New List(Of NodeRedUser)()
        Try
            Dim content As String = ReadSettingsFile()
            If String.IsNullOrWhiteSpace(content) Then Return result

            ' Estrai il blocco adminAuth
            Dim authBlockMatch As Match = Regex.Match(
                content,
                "adminAuth\s*:\s*\{([\s\S]*?)\},?\s*(?://|/\*|[a-zA-Z_])",
                RegexOptions.Multiline
            )
            If Not authBlockMatch.Success Then Return result

            Dim authBlock As String = authBlockMatch.Groups(1).Value

            ' Estrai ogni oggetto utente
            Dim userPattern As String =
                "\{\s*username\s*:\s*['""]([^'""]+)['""]\s*,\s*password\s*:\s*['""]([^'""]+)['""]\s*,\s*permissions\s*:\s*['""]([^'""]+)['""]\s*\}"
            Dim userMatches As MatchCollection = Regex.Matches(authBlock, userPattern, RegexOptions.Singleline)

            For Each m As Match In userMatches
                Dim u As New NodeRedUser()
                u.Username = m.Groups(1).Value
                u.PasswordHash = m.Groups(2).Value
                u.Permissions = m.Groups(3).Value
                result.Add(u)
            Next

            LogManager.AddInfo(String.Format("Utenti letti da settings.js: {0}", result.Count), "SecurityManager")

        Catch ex As Exception
            LogManager.AddError("Errore lettura utenti da settings.js: " & ex.Message, "SecurityManager")
        End Try
        Return result
    End Function

    ''' <summary>
    ''' Verifica se l autenticazione adminAuth e attiva e non commentata in settings.js.
    ''' </summary>
    Public Function GetAuthEnabled() As Boolean
        Try
            Dim content As String = ReadSettingsFile()
            If String.IsNullOrWhiteSpace(content) Then Return False
            Dim lines As String() = content.Split(New String() {Environment.NewLine, vbLf}, StringSplitOptions.None)
            For Each line In lines
                Dim trimmed As String = line.Trim()
                If trimmed.StartsWith("//") OrElse trimmed.StartsWith("*") Then Continue For
                If Regex.IsMatch(trimmed, "adminAuth\s*:") Then
                    Return True
                End If
            Next
            Return False
        Catch ex As Exception
            LogManager.AddError("Errore verifica adminAuth: " & ex.Message, "SecurityManager")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Verifica se la restrizione IP tramite httpAdminMiddleware e presente in settings.js.
    ''' </summary>
    Public Function GetIpRestrictionEnabled() As Boolean
        Try
            Dim content As String = ReadSettingsFile()
            If String.IsNullOrWhiteSpace(content) Then Return False
            Return Regex.IsMatch(content, "httpAdminMiddleware\s*:") AndAlso
                   content.Contains("Access denied")
        Catch ex As Exception
            LogManager.AddError("Errore verifica IP restriction: " & ex.Message, "SecurityManager")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Estrae la lista degli IP abilitati dalla sezione httpAdminMiddleware in settings.js.
    ''' </summary>
    Public Function GetAllowedIps() As List(Of String)
        Dim result As New List(Of String)()
        Try
            Dim content As String = ReadSettingsFile()
            If String.IsNullOrWhiteSpace(content) Then Return result

            Dim allowedMatch As Match = Regex.Match(
                content,
                "var\s+allowed\s*=\s*\[([\s\S]*?)\]",
                RegexOptions.Multiline
            )
            If Not allowedMatch.Success Then Return result

            Dim ipListContent As String = allowedMatch.Groups(1).Value
            Dim ipMatches As MatchCollection = Regex.Matches(ipListContent, "['""]([^'""]+)['""]")
            For Each m As Match In ipMatches
                Dim ip As String = m.Groups(1).Value.Trim()
                If Not String.IsNullOrWhiteSpace(ip) Then result.Add(ip)
            Next

        Catch ex As Exception
            LogManager.AddError("Errore lettura IP consentiti: " & ex.Message, "SecurityManager")
        End Try
        Return result
    End Function

#End Region

#Region "Hash Password"

    ''' <summary>
    ''' Genera un hash bcrypt di una password tramite Node.js e bcryptjs.
    ''' Esegue: node -e "const b=require('bcryptjs');b.hash('PASSWORD',8,function(_,h){console.log(h)})"
    ''' </summary>
    ''' <param name="password">Password in chiaro da hashare.</param>
    ''' <param name="nodePath">Percorso opzionale all eseguibile node. Usa AppSettings se non specificato.</param>
    ''' <returns>Hash bcrypt come stringa, o stringa vuota in caso di errore.</returns>
    Public Async Function HashPassword(password As String, Optional nodePath As String = "") As Task(Of String)
        Return Await Task.Run(Function()
            Try
                Dim nodeExe As String = nodePath
                If String.IsNullOrWhiteSpace(nodeExe) Then
                    nodeExe = AppSettings.Current.NodeExePath
                End If
                If String.IsNullOrWhiteSpace(nodeExe) Then nodeExe = "node"

                ' Sanitizza la password per uso in argomento command-line
                Dim safePassword As String = password.Replace("\", "\\").Replace("'", "\'").Replace("""", "\""")

                ' Script Node.js per hash bcrypt con saltRounds=8
                Dim script As String = String.Format(
                    "const b=require('bcryptjs');b.hash('{0}',8,function(_,h){{console.log(h)}})",
                    safePassword)

                Dim psi As New ProcessStartInfo()
                psi.FileName = nodeExe
                psi.Arguments = String.Format("-e ""{0}""", script)
                psi.UseShellExecute = False
                psi.RedirectStandardOutput = True
                psi.RedirectStandardError = True
                psi.CreateNoWindow = True

                Using proc As New Process()
                    proc.StartInfo = psi
                    proc.Start()

                    Dim output As String = proc.StandardOutput.ReadToEnd().Trim()
                    Dim errOutput As String = proc.StandardError.ReadToEnd().Trim()

                    proc.WaitForExit(15000)

                    If Not String.IsNullOrWhiteSpace(errOutput) Then
                        LogManager.AddWarn(String.Format("HashPassword stderr: {0}", errOutput), "SecurityManager")
                    End If

                    If Not String.IsNullOrWhiteSpace(output) AndAlso output.StartsWith("$2") Then
                        LogManager.AddInfo("Hash bcrypt generato con successo.", "SecurityManager")
                        Return output
                    End If

                    LogManager.AddError(String.Format("Hash bcrypt non valido. Output: {0}", output), "SecurityManager")
                    Return String.Empty
                End Using

            Catch ex As Exception
                LogManager.AddError(String.Format("Errore generazione hash bcrypt: {0}", ex.Message), "SecurityManager")
                Return String.Empty
            End Try
        End Function)
    End Function

#End Region

#Region "Scrittura Settings"

    ''' <summary>
    ''' Salva le impostazioni di sicurezza nel file settings.js di Node-RED.
    ''' Esegue backup preventivo, aggiorna i blocchi adminAuth e httpAdminMiddleware.
    ''' </summary>
    ''' <param name="users">Lista degli utenti per adminAuth.</param>
    ''' <param name="enabled">True per abilitare l autenticazione.</param>
    ''' <param name="ipRestrictionEnabled">True per abilitare la restrizione IP.</param>
    ''' <param name="allowedIps">Lista degli IP abilitati.</param>
    ''' <param name="ldapEnabled">True per abilitare LDAP.</param>
    ''' <param name="ldapUrl">URL del server LDAP.</param>
    ''' <param name="ldapBaseDn">Base DN per le query LDAP.</param>
    ''' <returns>True se il salvataggio e riuscito, False altrimenti.</returns>
    Public Async Function SaveSettings(
        users As List(Of NodeRedUser),
        enabled As Boolean,
        ipRestrictionEnabled As Boolean,
        allowedIps As List(Of String),
        Optional ldapEnabled As Boolean = False,
        Optional ldapUrl As String = "",
        Optional ldapBaseDn As String = "") As Task(Of Boolean)

        Return Await Task.Run(Function()
            Try
                Dim settingsPath As String = GetSettingsFilePath()

                ' 1. Backup del file corrente
                BackupSettingsFile()

                ' 2. Leggi contenuto attuale
                Dim content As String = ReadSettingsFile()

                ' 3. Se non esiste, genera settings.js minimale
                If String.IsNullOrWhiteSpace(content) Then
                    LogManager.AddInfo("settings.js non trovato. Generazione file minimale...", "SecurityManager")
                    GenerateMinimalSettingsJs(settingsPath)
                    content = ReadSettingsFile()
                    If String.IsNullOrWhiteSpace(content) Then
                        LogManager.AddError("Impossibile generare settings.js minimale.", "SecurityManager")
                        Return False
                    End If
                End If

                ' 4. Gestisci blocco adminAuth
                If enabled Then
                    Dim usersToSave As List(Of NodeRedUser) = users

                    ' Se nessun utente specificato, crea admin di default
                    If usersToSave Is Nothing OrElse usersToSave.Count = 0 Then
                        LogManager.AddWarn("Nessun utente specificato. Viene usato admin di default.", "SecurityManager")
                        Dim defaultUser As New NodeRedUser()
                        defaultUser.Username = "admin"
                        defaultUser.PasswordHash = "$2a$08$rMOdLx/Z.GXBnEFE4KOYKeSTFc7Jbp7e7TZ5QkWS4vIVrT9eTsOO"
                        defaultUser.Permissions = "full"
                        usersToSave = New List(Of NodeRedUser)() From {defaultUser}
                    End If

                    Dim authBlock As String = BuildAdminAuthBlock(usersToSave)
                    content = ReplaceOrInsertBlock(content, "adminAuth", authBlock)
                Else
                    content = CommentOutBlock(content, "adminAuth")
                End If

                ' 5. Gestisci blocco httpAdminMiddleware
                If ipRestrictionEnabled AndAlso allowedIps IsNot Nothing AndAlso allowedIps.Count > 0 Then
                    Dim middlewareBlock As String = BuildIpMiddlewareBlock(allowedIps)
                    content = ReplaceOrInsertBlock(content, "httpAdminMiddleware", middlewareBlock)
                Else
                    content = CommentOutBlock(content, "httpAdminMiddleware")
                End If

                ' 6. Scrivi il file aggiornato
                File.WriteAllText(settingsPath, content, Encoding.UTF8)
                LogManager.AddSuccess("settings.js aggiornato con successo.", "SecurityManager")
                Return True

            Catch ex As Exception
                LogManager.AddError(String.Format("Errore salvataggio settings.js: {0}", ex.Message), "SecurityManager")
                Return False
            End Try
        End Function)
    End Function

    ''' <summary>
    ''' Genera un file settings.js minimale con le impostazioni base di Node-RED.
    ''' adminAuth e commentato per default.
    ''' </summary>
    ''' <param name="targetPath">Percorso dove creare il file.</param>
    Public Sub GenerateMinimalSettingsJs(targetPath As String)
        Try
            Dim userDir As String = AppSettings.GetUserDir().Replace("\", "\\")
            Dim flowFile As String = AppSettings.Current.FlowFile
            If String.IsNullOrWhiteSpace(flowFile) Then flowFile = "flows.json"

            Dim sb As New StringBuilder()
            sb.AppendLine("module.exports = {")
            sb.AppendLine()
            sb.AppendLine("    // Porta HTTP di Node-RED")
            sb.AppendLine("    uiPort: process.env.PORT || 1880,")
            sb.AppendLine()
            sb.AppendLine("    // Directory utente di Node-RED")
            sb.AppendLine(String.Format("    userDir: '{0}',", userDir))
            sb.AppendLine()
            sb.AppendLine("    // File dei flussi")
            sb.AppendLine(String.Format("    flowFile: '{0}',", flowFile))
            sb.AppendLine()
            sb.AppendLine("    // Livello di log: fatal, error, warn, info, debug, trace")
            sb.AppendLine("    logging: {")
            sb.AppendLine("        console: {")
            sb.AppendLine("            level: 'info',")
            sb.AppendLine("            metrics: false,")
            sb.AppendLine("            audit: false")
            sb.AppendLine("        }")
            sb.AppendLine("    },")
            sb.AppendLine()
            sb.AppendLine("    // Impostazioni editor")
            sb.AppendLine("    editorTheme: {")
            sb.AppendLine("        projects: {")
            sb.AppendLine("            enabled: false")
            sb.AppendLine("        }")
            sb.AppendLine("    },")
            sb.AppendLine()
            sb.AppendLine("    // Autenticazione amministrativa (commentata, abilitare tramite NodeRedDesktop)")
            sb.AppendLine("    // adminAuth: {")
            sb.AppendLine("    //     type: 'credentials',")
            sb.AppendLine("    //     users: [{")
            sb.AppendLine("    //         username: 'admin',")
            sb.AppendLine("    //         password: '$2a$08$zZWtXTja0fB1pzD4sHCMyOCMYz2Z6dNbM6tl8sJogENOMcxWV9DN.',")
            sb.AppendLine("    //         permissions: '*'")
            sb.AppendLine("    //     }]")
            sb.AppendLine("    // },")
            sb.AppendLine()
            sb.AppendLine("    // Middleware restrizione IP (gestito da NodeRedDesktop)")
            sb.AppendLine("    // httpAdminMiddleware: function(req, res, next) { next(); },")
            sb.AppendLine()
            sb.AppendLine("}")

            Dim dir As String = Path.GetDirectoryName(targetPath)
            If Not Directory.Exists(dir) Then Directory.CreateDirectory(dir)
            File.WriteAllText(targetPath, sb.ToString(), Encoding.UTF8)
            LogManager.AddSuccess(String.Format("settings.js minimale generato: {0}", targetPath), "SecurityManager")

        Catch ex As Exception
            LogManager.AddError(String.Format("Errore generazione settings.js minimale: {0}", ex.Message), "SecurityManager")
        End Try
    End Sub

#End Region

#Region "Generazione Blocchi"

    ''' <summary>
    ''' Costruisce il blocco JavaScript per adminAuth con il tipo credentials.
    ''' </summary>
    ''' <param name="users">Lista degli utenti da includere.</param>
    ''' <returns>Stringa con il blocco adminAuth completo.</returns>
    Public Function BuildAdminAuthBlock(users As List(Of NodeRedUser)) As String
        Dim sb As New StringBuilder()
        sb.AppendLine("    adminAuth: {")
        sb.AppendLine("        type: 'credentials',")
        sb.AppendLine("        users: [")

        For i As Integer = 0 To users.Count - 1
            Dim u As NodeRedUser = users(i)
            Dim comma As String = If(i < users.Count - 1, ",", "")
            sb.AppendLine("            {")
            sb.AppendLine(String.Format("                username: '{0}',", u.Username))
            sb.AppendLine(String.Format("                password: '{0}',", u.PasswordHash))
            sb.AppendLine(String.Format("                permissions: '{0}'", u.Permissions))
            sb.AppendLine(String.Format("            }}{0}", comma))
        Next

        sb.AppendLine("        ]")
        sb.AppendLine("    },")
        Return sb.ToString()
    End Function

    ''' <summary>
    ''' Costruisce il blocco JavaScript per httpAdminMiddleware con whitelist IP.
    ''' Blocca tutti gli accessi non provenienti dagli IP consentiti (o da localhost).
    ''' </summary>
    ''' <param name="allowedIps">Lista di IP o subnet da consentire.</param>
    ''' <returns>Stringa con il blocco httpAdminMiddleware completo.</returns>
    Public Function BuildIpMiddlewareBlock(allowedIps As List(Of String)) As String
        Dim ipList As New StringBuilder()
        For i As Integer = 0 To allowedIps.Count - 1
            If i > 0 Then ipList.Append(", ")
            ipList.Append(String.Format("'{0}'", allowedIps(i)))
        Next

        Dim sb As New StringBuilder()
        sb.AppendLine("    httpAdminMiddleware: function(req, res, next) {")
        sb.AppendLine(String.Format("        var allowed = [{0}];", ipList.ToString()))
        sb.AppendLine("        var clientIp = req.ip || req.connection.remoteAddress || '';")
        sb.AppendLine("        var isAllowed = allowed.some(function(ip) { return clientIp.indexOf(ip) !== -1; })")
        sb.AppendLine("                     || clientIp === '::1'")
        sb.AppendLine("                     || clientIp === '127.0.0.1';")
        sb.AppendLine("        if (isAllowed) {")
        sb.AppendLine("            next();")
        sb.AppendLine("        } else {")
        sb.AppendLine("            res.status(403).json({ error: 'Access denied' });")
        sb.AppendLine("        }")
        sb.AppendLine("    },")
        Return sb.ToString()
    End Function

    ''' <summary>
    ''' Sostituisce un blocco esistente in settings.js oppure lo inserisce
    ''' prima della chiusura del module.exports se non presente.
    ''' </summary>
    Private Function ReplaceOrInsertBlock(content As String, blockKey As String, newBlock As String) As String
        Try
            If Regex.IsMatch(content, blockKey & "\s*:") Then
                ' Blocco esistente: sostituisci con regex
                content = Regex.Replace(
                    content,
                    String.Format("(?:\s*//[^\n]*\n)*\s*(?://\s*)?{0}\s*:[\s\S]*?(?=\n\s*(?:\/\/\s*)?[a-zA-Z_$]|\n\s*\}})", Regex.Escape(blockKey)),
                    Environment.NewLine & "    " & newBlock.TrimEnd(),
                    RegexOptions.Multiline
                )
            Else
                ' Blocco non presente: inserisci prima della chiusura "}"
                Dim insertPoint As Integer = content.LastIndexOf("}")
                If insertPoint >= 0 Then
                    content = content.Substring(0, insertPoint) &
                              Environment.NewLine & "    " & newBlock.TrimEnd() &
                              Environment.NewLine & content.Substring(insertPoint)
                End If
            End If
            Return content
        Catch ex As Exception
            LogManager.AddWarn(String.Format("ReplaceOrInsertBlock '{0}': {1}", blockKey, ex.Message), "SecurityManager")
            Return content
        End Try
    End Function

    ''' <summary>
    ''' Commenta un blocco chiave in settings.js aggiungendo // alle righe del blocco.
    ''' Se il blocco non esiste, non fa nulla.
    ''' </summary>
    Private Function CommentOutBlock(content As String, blockKey As String) As String
        Try
            If Not Regex.IsMatch(content, blockKey & "\s*:") Then
                Return content
            End If

            Dim lines As String() = content.Split(New String() {Environment.NewLine, vbLf}, StringSplitOptions.None)
            Dim result As New StringBuilder()
            Dim inBlock As Boolean = False
            Dim braceDepth As Integer = 0

            For Each line In lines
                Dim trimmed As String = line.Trim()

                If Not inBlock Then
                    If Not trimmed.StartsWith("//") AndAlso Regex.IsMatch(trimmed, "^" & Regex.Escape(blockKey) & "\s*:") Then
                        inBlock = True
                        braceDepth += line.Split("{"c).Length - 1
                        braceDepth -= line.Split("}"c).Length - 1
                        result.AppendLine("    // " & line.TrimStart())
                        If braceDepth <= 0 Then inBlock = False
                        Continue For
                    End If
                Else
                    braceDepth += line.Split("{"c).Length - 1
                    braceDepth -= line.Split("}"c).Length - 1
                    result.AppendLine("    // " & line.TrimStart())
                    If braceDepth <= 0 Then inBlock = False
                    Continue For
                End If

                result.AppendLine(line)
            Next

            Return result.ToString()
        Catch ex As Exception
            LogManager.AddWarn(String.Format("CommentOutBlock '{0}': {1}", blockKey, ex.Message), "SecurityManager")
            Return content
        End Try
    End Function

#End Region

End Module

End Namespace
