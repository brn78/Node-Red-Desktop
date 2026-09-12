# Node-RED Desktop

[![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.8-512BD4?style=flat&logo=dotnet)](https://dotnet.microsoft.com/)
[![Language](https://img.shields.io/badge/Language-VB.NET-00539C?style=flat&logo=visualbasic)](https://learn.microsoft.com/dotnet/visual-basic/)
[![Platform](https://img.shields.io/badge/Platform-Windows-0078D6?style=flat&logo=windows)](https://www.microsoft.com/windows)
[![UI](https://img.shields.io/badge/UI-Windows%20Forms%20(Designer--First)-red?style=flat)](https://learn.microsoft.com/visualstudio/designers/)

Applicazione desktop nativa per Windows sviluppata in **VB.NET (.NET Framework 4.8 / Windows Forms)** per il controllo completo, monitoraggio e gestione del ciclo di vita di **Node-RED** su workstation e server aziendali.

---

## 🌟 Caratteristiche Principali

- **🎨 Architettura Designer-First**:
  - Tutti gli elementi dell'interfaccia sono definiti direttamente nel file `MainForm.Designer.vb` di Visual Studio.
  - Nessuna creazione forzata di controlli a runtime: l'interfaccia è modificabile, riposizionabile e personalizzabile al 100% dal visual designer grafico di Visual Studio.
- **⚡ Motore Asincrono Non Bloccante**:
  - Avvio, arresto, riavvio e auto-rilevamento gestiti con `Async/Await` e thread di background.
  - La finestra e la pompa dei messaggi di Windows rimangono costantemente reattive (avvio in meno di 170 ms).
  - Verifica della porta TCP non bloccante via `IPGlobalProperties` a 0 ms.
  - Arresto pulito dell'albero dei processi (`taskkill /T /F`) per evitare processi `node.exe` orfani.
- **🔌 Smart Attach Automatico**:
  - Se un'istanza di Node-RED è già in ascolto sulla porta configurata (es. 1880), l'applicazione la rileva e si aggancia automaticamente al processo esistente visualizzandone PID, uptime e risorse.
- **🐕 Watchdog & Auto-Recovery**:
  - Monitoraggio costante del processo. In caso di interruzione anomala o crash, il watchdog riavvia automaticamente il servizio rispettando la soglia massima configurata (`MaxRestarts`).
- **💾 Gestione Backup & Ripristino**:
  - Creazione di archivi ZIP completi della cartella dati (`flows.json`, `flows_cred.json`, `settings.js`, `package.json`).
  - Schedulazione automatica: disabilitata, oraria, giornaliera o settimanale.
  - Criteri di retention automatica per mantenere solo gli ultimi *N* backup.
  - Ripristino con un clic con backup di sicurezza preventivo automatico.
- **🔐 Sicurezza & Controllo Accessi (`settings.js`)**:
  - Abilitazione/disabilitazione login con credenziali per l'editor web (`adminAuth`).
  - Generazione e calcolo automatico di hash password protetti con **bcrypt**.
  - Restrizione accesso per indirizzo IP (whitelist IP via `httpAdminMiddleware`).
  - Supporto per autenticazione LDAP / Active Directory aziendale.
- **📜 Log in Tempo Reale & Esportazione**:
  - Visualizzatore di log integrato con colorazione semantica per livello (INFO, AVVISI, ERRORI, DEBUG, OK).
  - Pausa, pulizia, filtro per severità e scorrimento automatico.
  - Esportazione istantanea dello storico su file `.txt` e `.csv`.
- **🛎️ Notifiche Toast & System Tray**:
  - Notifiche toast non modali con animazione di fade-in/fade-out ed auto-dismissal.
  - Icona nell'area di notifica (System Tray) con menu contestuale per avvio, stop, riavvio rapido e apertura browser.
  - Opzione per minimizzare nella barra delle applicazioni alla chiusura.

---

## 🗂️ Struttura della Soluzione

```text
NodeRedDesktop/
├── NodeRedDesktop.sln                     # Soluzione Visual Studio
├── .gitignore                             # Regole di esclusione build e cache
├── README.md                              # Documentazione di progetto
└── NodeRedDesktop/
    ├── NodeRedDesktop.vbproj              # Progetto VB.NET WinForms
    ├── Program.vb                         # Entry point dell'applicazione (Sub Main)
    ├── Forms/
    │   ├── MainForm.vb                    # Logica applicativa principale
    │   ├── MainForm.Designer.vb           # Definizione controlli Visual Studio Designer
    │   ├── MainForm.resx                  # Risorse grafiche della form principale
    │   ├── ToastForm.vb                   # Notifiche toast animate
    │   └── ToastForm.Designer.vb          # Layout grafico del toast
    ├── Helpers/
    │   └── UIHelper.vb                    # Costanti tema Dark, palette colori e stili
    ├── Modules/
    │   ├── NodeManager.vb                 # Gestione processo Node-RED, watchdog e porte
    │   ├── DependencyChecker.vb           # Verifica e installazione Node.js / npm / Node-RED
    │   ├── StartupManager.vb              # Autoavvio Windows (Registry e Task Scheduler)
    │   ├── BackupManager.vb               # Schedulazione, creazione ZIP e ripristino
    │   ├── SecurityManager.vb             # Gestione settings.js, bcrypt e restrizioni IP
    │   ├── UpdateChecker.vb               # Verifica aggiornamenti da registry npm
    │   └── LogManager.vb                  # Coda log thread-safe ed esportazione
    ├── Services/
    │   └── AppSettings.vb                 # Configurazione XML serializzabile e auto-detect percorsi
    └── Resources/
        ├── nodered.ico                    # Icona applicativa
        └── nodered.png                    # Logo grafico per l'header
```

---

## ⚙️ Requisiti di Sistema

- **Sistema Operativo**: Windows 10, Windows 11, Windows Server 2016 o versioni successive.
- **Framework**: .NET Framework 4.8 o versione superiore.
- **Ambiente di Esecuzione**: Node.js (v18+) e Node-RED (v2.0+) installati sul sistema.

---

## 🚀 Compilazione con MSBuild

Per compilare la soluzione da riga di comando:

```powershell
# Compilazione Release
msbuild NodeRedDesktop.sln /t:Rebuild /p:Configuration=Release

# Compilazione Debug
msbuild NodeRedDesktop.sln /t:Rebuild /p:Configuration=Debug
```

L'eseguibile generato sarà disponibile in `NodeRedDesktop\bin\Release\Node-RED Desktop.exe`.

---

## 📄 Licenza

Questo software è protetto da licenza privata proprietaria. Tutti i diritti riservati.
