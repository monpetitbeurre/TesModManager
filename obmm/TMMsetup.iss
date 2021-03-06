; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "TesModManager"
#define MyAppVersion "1.6.0"
#define MyAppPublisher "monpetitbeurre"
#define MyAppURL "http://www.nexusmods.com/skyrim/mods/5010"
#define MyAppExeName "TesModManager.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{CDE04319-6124-4C6F-A63B-FA60E132F563}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}
DisableProgramGroupPage=yes
OutputDir=..\Install
OutputBaseFilename=TMMsetup
SetupIconFile=.\icon.ico
Compression=lzma
SolidCompression=yes
UninstallDisplayName=TesModManager
UninstallDisplayIcon={app}\TesModManager.exe
VersionInfoVersion=1.6.56
VersionInfoDescription=Mod manager for Skyrim, Oblivion and Morrowind
VersionInfoTextVersion=1.6.56
VersionInfoProductName=TesModManager
VersionInfoProductVersion=1.6.56
VersionInfoProductTextVersion=1.6.56

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "skyrimshortcut"; Description: "TMM for Skyrim shortcut on desktop"; Components: TesModManagerforSkyrim
Name: "skyrimseshortcut"; Description: "TMM for Skyrim SE shortcut on desktop"; Components: TesModManagerForSkyrimSE
Name: "oblivionshortcut"; Description: "TMM for Oblivion shortcut on desktop"; Components: TesModManagerforOblivion
Name: "morrowindshortcut"; Description: "TMM for Morrowind shortcut on desktop"; Components: TesModManagerforMorrowind

[Files]
Source: "..\Release\TesModManager.exe"; DestDir: "{app}"; Flags: ignoreversion; Components: TesModManagerforMorrowind TesModManagerforOblivion TesModManagerforSkyrim TesModManagerForSkyrimSE
Source: "..\Release\HtmlAgilityPack.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: TesModManagerforMorrowind TesModManagerforOblivion TesModManagerforSkyrim TesModManagerForSkyrimSE
Source: "..\Release\7z.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: TesModManagerforMorrowind TesModManagerforOblivion TesModManagerforSkyrim TesModManagerForSkyrimSE
Source: "..\Release\BaseTools.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: TesModManagerforMorrowind TesModManagerforOblivion TesModManagerforSkyrim TesModManagerForSkyrimSE
Source: "..\Release\BaseTools.xml"; DestDir: "{app}"; Flags: ignoreversion; Components: TesModManagerforMorrowind TesModManagerforOblivion TesModManagerforSkyrim TesModManagerForSkyrimSE
Source: "..\Release\DevIL.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: TesModManagerforMorrowind TesModManagerforOblivion TesModManagerforSkyrim TesModManagerForSkyrimSE
Source: "..\Release\DevIlDotNet.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: TesModManagerforMorrowind TesModManagerforOblivion TesModManagerforSkyrim TesModManagerForSkyrimSE
Source: "..\Release\DevILNet.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: TesModManagerforMorrowind TesModManagerforOblivion TesModManagerforSkyrim TesModManagerForSkyrimSE
Source: "..\Release\il.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: TesModManagerforMorrowind TesModManagerforOblivion TesModManagerforSkyrim TesModManagerForSkyrimSE
Source: "..\Release\ILU.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: TesModManagerforMorrowind TesModManagerforOblivion TesModManagerforSkyrim TesModManagerForSkyrimSE
Source: "..\Release\IronMath.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: TesModManagerforMorrowind TesModManagerforOblivion TesModManagerforSkyrim TesModManagerForSkyrimSE
Source: "..\Release\IronPython.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: TesModManagerforMorrowind TesModManagerforOblivion TesModManagerforSkyrim TesModManagerForSkyrimSE
Source: "..\Release\SharpCompress.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: TesModManagerforMorrowind TesModManagerforOblivion TesModManagerforSkyrim TesModManagerForSkyrimSE
Source: "..\Release\msvcp110.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: TesModManagerforMorrowind TesModManagerforOblivion TesModManagerforSkyrim TesModManagerForSkyrimSE
Source: "..\Release\msvcr110.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: TesModManagerforMorrowind TesModManagerforOblivion TesModManagerforSkyrim TesModManagerForSkyrimSE
Source: "..\Release\HtmlAgilityPack.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: TesModManagerforMorrowind TesModManagerforOblivion TesModManagerforSkyrim TesModManagerForSkyrimSE
Source: "..\Release\HtmlAgilityPack.xml"; DestDir: "{app}"; Flags: ignoreversion; Components: TesModManagerforMorrowind TesModManagerforOblivion TesModManagerforSkyrim TesModManagerForSkyrimSE
Source: "..\Release\SevenZipSharp.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: TesModManagerforMorrowind TesModManagerforOblivion TesModManagerforSkyrim TesModManagerForSkyrimSE
Source: "..\Release\Tao.DevIl.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: TesModManagerforMorrowind TesModManagerforOblivion TesModManagerforSkyrim TesModManagerForSkyrimSE
Source: "..\Release\TMM-readme.txt"; DestDir: "{app}"; Flags: ignoreversion; Components: TesModManagerforMorrowind TesModManagerforOblivion TesModManagerforSkyrim TesModManagerForSkyrimSE
Source: "..\Release\unrar.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: TesModManagerforMorrowind TesModManagerforOblivion TesModManagerforSkyrim TesModManagerForSkyrimSE
Source: "..\Release\vccorlib110.dll"; DestDir: "{app}"; Flags: ignoreversion; Components: TesModManagerforMorrowind TesModManagerforOblivion TesModManagerforSkyrim TesModManagerForSkyrimSE
; NOTE: Don't use "Flags: ignoreversion" on any shared system files
Source: "..\Release\obmm\obmm.chm"; DestDir: "{app}"; DestName: "obmm.chm"

[Icons]
Name: "{commonprograms}\TesModManager for Skyrim"; Filename: "{app}\{#MyAppExeName}"; IconFilename: "{app}\TesModManager.exe"; IconIndex: 0; Parameters: "Skyrim"; Components: TesModManagerforSkyrim; Tasks: skyrimshortcut
Name: "{commonprograms}\TesModManager for Skyrim Special Edition"; Filename: "{app}\{#MyAppExeName}"; IconFilename: "{app}\TesModManager.exe"; IconIndex: 0; Parameters: "SkyrimSE"; Components: TesModManagerForSkyrimSE; Tasks: skyrimseshortcut
Name: "{commonprograms}\TesModManager for Oblivion"; Filename: "{app}\{#MyAppExeName}"; IconFilename: "{app}\TesModManager.exe"; IconIndex: 0; Parameters: "Oblivion"; Components: TesModManagerforOblivion; Tasks: oblivionshortcut
Name: "{commonprograms}\TesModManager for Morrowind"; Filename: "{app}\{#MyAppExeName}"; IconFilename: "{app}\TesModManager.exe"; IconIndex: 0; Parameters: "Morrowind"; Components: TesModManagerforMorrowind; Tasks: morrowindshortcut
Name: "{commondesktop}\TesModManager for Skyrim"; Filename: "{app}\{#MyAppExeName}"; Parameters: "Skyrim"; Components: TesModManagerforSkyrim; Tasks: skyrimshortcut
Name: "{commondesktop}\TesModManager for Skyrim Special Edition"; Filename: "{app}\{#MyAppExeName}"; Parameters: "SkyrimSE"; Components: TesModManagerForSkyrimSE; Tasks: skyrimseshortcut
Name: "{commondesktop}\TesModManager for Oblivion"; Filename: "{app}\{#MyAppExeName}"; Parameters: "Oblivion"; Components: TesModManagerforOblivion; Tasks: oblivionshortcut
Name: "{commondesktop}\TesModManager for Morrowind"; Filename: "{app}\{#MyAppExeName}"; Parameters: "Morrowind"; Components: TesModManagerforMorrowind; Tasks: morrowindshortcut

[Run]
; Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[Registry]
Root: "HKLM"; Subkey: "Software\Classes\nxm"; ValueType: string; ValueData: """URL: Nexus mod protocol"""; Flags: createvalueifdoesntexist noerror uninsdeletekey
Root: "HKLM"; Subkey: "Software\Classes\nxm"; ValueType: string; ValueName: """URL Protocol"""; Flags: createvalueifdoesntexist uninsdeletekey
Root: "HKLM"; Subkey: "Software\Classes\nxm\DefaultIcon"; ValueType: string; ValueName: "DefaultIcon"; ValueData: "{app}\{#MyAppName},0"; Flags: createvalueifdoesntexist uninsdeletekey
Root: "HKLM"; Subkey: "Software\Classes\nxm\shell\open\command"; ValueType: string; ValueData: """{app}\{#MyAppName}"" ""%1"""; Flags: createvalueifdoesntexist uninsdeletekey
Root: HKCR; SubKey: ".omod"; ValueType: string; ValueData: "Tes Mod file"; Flags: uninsdeletekey
Root: HKCR; SubKey: "Tes Mod file"; ValueType: string; ValueData: "Mod package for use with Oblivion Mod Manager (OBMM) and Tes Mod Manager (TMM)"; Flags: uninsdeletekey
Root: HKCR; SubKey: "Tes Mod file\Shell\Open\Command"; ValueType: string; ValueData: """{app}\TesModManager.exe"" ""%1"""; Flags: uninsdeletekey
Root: HKCR; Subkey: "Tes Mod file\DefaultIcon"; ValueType: string; ValueData: "{app}\TesModManager.exe,0"; Flags: uninsdeletevalue
Root: HKCR; SubKey: ".omod2"; ValueType: string; ValueData: "Mod package"; Flags: uninsdeletekey
Root: HKCR; SubKey: "Mod package"; ValueType: string; ValueData: "Mod package associated with Tes Mod Manager (TMM)"; Flags: uninsdeletekey
Root: HKCR; SubKey: "Mod package\Shell\Open\Command"; ValueType: string; ValueData: """{app}\TesModManager.exe"" ""%1"""; Flags: uninsdeletekey
Root: HKCR; Subkey: "Mod package\DefaultIcon"; ValueType: string; ValueData: "{app}\TesModManager.exe,0"; Flags: uninsdeletevalue

[Components]
Name: "TesModManagerforSkyrim"; Description: "TesModManager for Skyrim"; Types: compact custom full; Flags: checkablealone
Name: "TesModManagerforOblivion"; Description: "TesModManager for Oblivion"; Types: compact custom full; Flags: checkablealone
Name: "TesModManagerforMorrowind"; Description: "TesModManager for Morrowind"; Types: compact custom full; Flags: checkablealone
Name: "TesModManagerForSkyrimSE"; Description: "TesModManager for Skyrim Special Edition"; Types: compact custom full; Flags: checkablealone
