<#
.SYNOPSIS
    Compila Node-RED Desktop in Release, genera l'installer Inno Setup e il relativo hash SHA-256.

.DESCRIPTION
    Produce in ..\dist:
      - Node-RED-Desktop-Setup.exe          (installer)
      - Node-RED-Desktop-Setup.exe.sha256   (hash, formato "HASH *nomefile" compatibile sha256sum)

    ENTRAMBI i file vanno caricati come asset della release GitHub: l'auto-update
    dell'applicazione verifica l'hash prima di eseguire l'installer scaricato e, in
    assenza di hash pubblicato, accetta solo installer firmati con Authenticode.

.PARAMETER Version
    Versione da usare (es. 1.1.0). Se omessa viene letta da AssemblyInfo.vb.

.PARAMETER SignTool / CertThumbprint
    Opzionali: se indicati, firma l'installer con signtool.exe e il certificato specificato.

.EXAMPLE
    .\build-release.ps1
    .\build-release.ps1 -Version 1.1.0 -SignTool "C:\Program Files (x86)\Windows Kits\10\bin\x64\signtool.exe" -CertThumbprint ABCDEF...
#>
[CmdletBinding()]
param(
    [string]$Version,
    [string]$MsBuild,
    [string]$Iscc,
    [string]$SignTool,
    [string]$CertThumbprint
)

$ErrorActionPreference = 'Stop'
$root = Split-Path -Parent $PSScriptRoot
$sln  = Join-Path $root 'NodeRedDesktop.sln'
$iss  = Join-Path $PSScriptRoot 'NodeRedDesktop.iss'
$dist = Join-Path $root 'dist'
$exeName = 'Node-RED-Desktop-Setup.exe'

function Find-Tool([string]$explicit, [string[]]$candidates, [string]$name) {
    if ($explicit -and (Test-Path $explicit)) { return $explicit }
    $cmd = Get-Command $name -ErrorAction SilentlyContinue
    if ($cmd) { return $cmd.Source }
    foreach ($c in $candidates) { $hits = Get-Item $c -ErrorAction SilentlyContinue | Sort-Object FullName -Descending; if ($hits) { return $hits[0].FullName } }
    throw "$name non trovato: specificarlo con il parametro dedicato."
}

$MsBuild = Find-Tool $MsBuild @(
    "${env:ProgramFiles}\Microsoft Visual Studio\*\*\MSBuild\Current\Bin\MSBuild.exe",
    "${env:ProgramFiles(x86)}\Microsoft Visual Studio\*\*\MSBuild\Current\Bin\MSBuild.exe"
) 'MSBuild.exe'
$Iscc = Find-Tool $Iscc @(
    "${env:ProgramFiles(x86)}\Inno Setup 6\ISCC.exe",
    "${env:ProgramFiles}\Inno Setup 6\ISCC.exe"
) 'ISCC.exe'

# Versione: parametro oppure AssemblyVersion in AssemblyInfo.vb
if (-not $Version) {
    $asmInfo = Get-Content (Join-Path $root 'NodeRedDesktop\My Project\AssemblyInfo.vb') -Raw
    if ($asmInfo -match 'AssemblyVersion\("(\d+)\.(\d+)\.(\d+)') { $Version = "$($Matches[1]).$($Matches[2]).$($Matches[3])" }
    else { throw 'Impossibile determinare la versione da AssemblyInfo.vb' }
}
Write-Host "==> Node-RED Desktop v$Version" -ForegroundColor Cyan

Write-Host '==> Compilazione Release (MSBuild)...' -ForegroundColor Cyan
& $MsBuild $sln /nologo /v:m /t:Rebuild /p:Configuration=Release
if ($LASTEXITCODE -ne 0) { throw "MSBuild ha restituito $LASTEXITCODE" }

Write-Host '==> Creazione installer (Inno Setup)...' -ForegroundColor Cyan
& $Iscc /Q "/DMyAppVersion=$Version" $iss
if ($LASTEXITCODE -ne 0) { throw "ISCC ha restituito $LASTEXITCODE" }

$setup = Join-Path $dist $exeName
if (-not (Test-Path $setup)) { throw "Installer non trovato: $setup" }

if ($SignTool -and $CertThumbprint) {
    Write-Host '==> Firma Authenticode dell''installer...' -ForegroundColor Cyan
    & $SignTool sign /sha1 $CertThumbprint /fd SHA256 /tr http://timestamp.digicert.com /td SHA256 $setup
    if ($LASTEXITCODE -ne 0) { throw "signtool ha restituito $LASTEXITCODE" }
}

Write-Host '==> Calcolo SHA-256...' -ForegroundColor Cyan
$hash = (Get-FileHash -Algorithm SHA256 $setup).Hash.ToLowerInvariant()
$hashFile = "$setup.sha256"
# Formato sha256sum: "<hash> *<file>" (asterisco = modalita' binaria), newline LF, senza BOM
[System.IO.File]::WriteAllText($hashFile, "$hash *$exeName`n", (New-Object System.Text.UTF8Encoding($false)))

Write-Host ''
Write-Host "Installer : $setup" -ForegroundColor Green
Write-Host "SHA-256   : $hash" -ForegroundColor Green
Write-Host "Hash file : $hashFile" -ForegroundColor Green
Write-Host ''
Write-Host "Carica ENTRAMBI i file come asset della release GitHub v$Version." -ForegroundColor Yellow
