; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "Scheduler"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "Kozitsky Ilia"
#define MyAppURL "https://github.com/lasedra/Scheduler_graduatework"
#define MyAppExeName "Scheduler.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{EEDE3FFA-AA2F-40D2-81E7-226EDCE291F2}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
; "ArchitecturesAllowed=x64compatible" specifies that Setup cannot run
; on anything but x64 and Windows 11 on Arm.
ArchitecturesAllowed=x64compatible
; "ArchitecturesInstallIn64BitMode=x64compatible" requests that the
; install be done in "64-bit mode" on x64 or Windows 11 on Arm,
; meaning it should use the native 64-bit Program Files directory and
; the 64-bit view of the registry.
ArchitecturesInstallIn64BitMode=x64compatible
DisableProgramGroupPage=yes
InfoBeforeFile=C:\Users\.eski\Desktop\DbInitScript.txt
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
OutputDir=C:\Users\.eski\Downloads
OutputBaseFilename=SchedulerSetup
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "russian"; MessagesFile: "compiler:Languages\Russian.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "C:\Users\.eski\Desktop\4 КУРС\ДИПЛОМ\Scheduler\Scheduler\bin\Release\net7.0-windows\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\.eski\Desktop\4 КУРС\ДИПЛОМ\Scheduler\Scheduler\bin\Release\net7.0-windows\Microsoft.EntityFrameworkCore.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\.eski\Desktop\4 КУРС\ДИПЛОМ\Scheduler\Scheduler\bin\Release\net7.0-windows\Microsoft.EntityFrameworkCore.Relational.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\.eski\Desktop\4 КУРС\ДИПЛОМ\Scheduler\Scheduler\bin\Release\net7.0-windows\Microsoft.Extensions.Caching.Abstractions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\.eski\Desktop\4 КУРС\ДИПЛОМ\Scheduler\Scheduler\bin\Release\net7.0-windows\Microsoft.Extensions.Caching.Memory.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\.eski\Desktop\4 КУРС\ДИПЛОМ\Scheduler\Scheduler\bin\Release\net7.0-windows\Microsoft.Extensions.Configuration.Abstractions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\.eski\Desktop\4 КУРС\ДИПЛОМ\Scheduler\Scheduler\bin\Release\net7.0-windows\Microsoft.Extensions.Configuration.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\.eski\Desktop\4 КУРС\ДИПЛОМ\Scheduler\Scheduler\bin\Release\net7.0-windows\Microsoft.Extensions.Configuration.FileExtensions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\.eski\Desktop\4 КУРС\ДИПЛОМ\Scheduler\Scheduler\bin\Release\net7.0-windows\Microsoft.Extensions.Configuration.Json.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\.eski\Desktop\4 КУРС\ДИПЛОМ\Scheduler\Scheduler\bin\Release\net7.0-windows\Microsoft.Extensions.DependencyInjection.Abstractions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\.eski\Desktop\4 КУРС\ДИПЛОМ\Scheduler\Scheduler\bin\Release\net7.0-windows\Microsoft.Extensions.DependencyInjection.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\.eski\Desktop\4 КУРС\ДИПЛОМ\Scheduler\Scheduler\bin\Release\net7.0-windows\Microsoft.Extensions.DependencyModel.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\.eski\Desktop\4 КУРС\ДИПЛОМ\Scheduler\Scheduler\bin\Release\net7.0-windows\Microsoft.Extensions.FileProviders.Abstractions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\.eski\Desktop\4 КУРС\ДИПЛОМ\Scheduler\Scheduler\bin\Release\net7.0-windows\Microsoft.Extensions.FileProviders.Physical.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\.eski\Desktop\4 КУРС\ДИПЛОМ\Scheduler\Scheduler\bin\Release\net7.0-windows\Microsoft.Extensions.FileSystemGlobbing.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\.eski\Desktop\4 КУРС\ДИПЛОМ\Scheduler\Scheduler\bin\Release\net7.0-windows\Microsoft.Extensions.Logging.Abstractions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\.eski\Desktop\4 КУРС\ДИПЛОМ\Scheduler\Scheduler\bin\Release\net7.0-windows\Microsoft.Extensions.Logging.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\.eski\Desktop\4 КУРС\ДИПЛОМ\Scheduler\Scheduler\bin\Release\net7.0-windows\Microsoft.Extensions.Options.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\.eski\Desktop\4 КУРС\ДИПЛОМ\Scheduler\Scheduler\bin\Release\net7.0-windows\Microsoft.Extensions.Primitives.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\.eski\Desktop\4 КУРС\ДИПЛОМ\Scheduler\Scheduler\bin\Release\net7.0-windows\Mono.TextTemplating.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\.eski\Desktop\4 КУРС\ДИПЛОМ\Scheduler\Scheduler\bin\Release\net7.0-windows\Npgsql.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\.eski\Desktop\4 КУРС\ДИПЛОМ\Scheduler\Scheduler\bin\Release\net7.0-windows\Npgsql.EntityFrameworkCore.PostgreSQL.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\.eski\Desktop\4 КУРС\ДИПЛОМ\Scheduler\Scheduler\bin\Release\net7.0-windows\Scheduler.deps.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\.eski\Desktop\4 КУРС\ДИПЛОМ\Scheduler\Scheduler\bin\Release\net7.0-windows\Scheduler.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\.eski\Desktop\4 КУРС\ДИПЛОМ\Scheduler\Scheduler\bin\Release\net7.0-windows\Scheduler.dll.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\.eski\Desktop\4 КУРС\ДИПЛОМ\Scheduler\Scheduler\bin\Release\net7.0-windows\Scheduler.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\.eski\Desktop\4 КУРС\ДИПЛОМ\Scheduler\Scheduler\bin\Release\net7.0-windows\Scheduler.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\.eski\Desktop\4 КУРС\ДИПЛОМ\Scheduler\Scheduler\bin\Release\net7.0-windows\Scheduler.runtimeconfig.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\.eski\Desktop\4 КУРС\ДИПЛОМ\Scheduler\Scheduler\bin\Release\net7.0-windows\Xceed.Document.NET5.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\.eski\Desktop\4 КУРС\ДИПЛОМ\Scheduler\Scheduler\bin\Release\net7.0-windows\Xceed.Words.NET5.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\.eski\Desktop\4 КУРС\ДИПЛОМ\Scheduler\Scheduler\bin\Release\net7.0-windows\appconfig.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\.eski\Desktop\4 КУРС\ДИПЛОМ\Scheduler\Scheduler\bin\Release\net7.0-windows\Humanizer.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\.eski\Desktop\4 КУРС\ДИПЛОМ\Scheduler\Scheduler\bin\Release\net7.0-windows\Microsoft.EntityFrameworkCore.Abstractions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\.eski\Desktop\4 КУРС\ДИПЛОМ\Scheduler\Scheduler\bin\Release\net7.0-windows\Microsoft.EntityFrameworkCore.Design.dll"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

