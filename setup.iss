[Setup]
AppName=CPS Booster
AppVersion=1.0
DefaultDirName={pf}\CPS Booster
DefaultGroupName=CPS Booster
OutputBaseFilename=Setup_CPS_Booster
Compression=lzma
SolidCompression=yes
; Menentukan ikon untuk installer
SetupIconFile=Assets\cpsbooster-logo.ico

[Files]
Source: "bin\Release\net8.0\win-x64\publish\CPS Booster.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "bin\Release\net8.0\win-x64\publish\*.dll"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
; Menentukan ikon untuk shortcut di menu Start dan desktop
Name: "{group}\CPS Booster"; Filename: "{app}\CPS Booster.exe"; IconFilename: "{src}\Assets\cpsbooster-logo.ico"
Name: "{userdesktop}\CPS Booster"; Filename: "{app}\CPS Booster.exe"; IconFilename: "{src}\Assets\cpsbooster-logo.ico"
