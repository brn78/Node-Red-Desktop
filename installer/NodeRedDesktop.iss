; Script Inno Setup (6.x / 7.x) per Node-RED Desktop
; La versione si passa da riga di comando: ISCC /DMyAppVersion=X.Y.Z NodeRedDesktop.iss

#define MyAppName "Node-RED Desktop"
#ifndef MyAppVersion
  #define MyAppVersion "1.0.0"
#endif
#define MyAppPublisher "Bruno Leonardi"
#define MyAppURL "https://github.com/brn78/Node-Red-Desktop"
#define MyAppExeName "Node-RED Desktop.exe"

[Setup]
AppId={{A31A8B35-30FE-4B38-A635-DE5E6E66B141}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}/issues
AppUpdatesURL={#MyAppURL}/releases
DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppName}
AllowNoIcons=yes
OutputDir=..\dist
OutputBaseFilename=Node-RED-Desktop-Setup
SetupIconFile=..\NodeRedDesktop\Resources\nodered.ico
UninstallDisplayIcon={app}\{#MyAppExeName}
Compression=lzma2/ultra64
SolidCompression=yes
WizardStyle=modern
; Installer x86 (default): adatto all'applicazione AnyCPU su .NET Framework 4.8
ArchitecturesAllowed=x86compatible
TimeStampsInUTC=yes
DisableProgramGroupPage=yes
PrivilegesRequired=lowest
PrivilegesRequiredOverridesAllowed=dialog
CloseApplications=yes
RestartApplications=no

[Languages]
Name: "italian"; MessagesFile: "compiler:Languages\Italian.isl"
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"

[Files]
Source: "..\NodeRedDesktop\bin\Release\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\NodeRedDesktop\bin\Release\Node-RED Desktop.xml"; DestDir: "{app}"; Flags: ignoreversion skipifsourcedoesntexist
Source: "..\NodeRedDesktop\bin\Release\Resources\*"; DestDir: "{app}\Resources"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent
