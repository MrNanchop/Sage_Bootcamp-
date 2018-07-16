[Setup]
AllowUNCPath="no"
AlwaysShowDirOnReadyPage="yes"
AppId=SetupSeminarverwaltung80
AppName=Sage 100 / Office Line - Seminarverwaltung
AppVerName=Sage 100 / Office Line - Seminarverwaltung
Compression=lzma
DefaultDirName={code:GetRoot}
DisableDirPage=yes
DisableProgramGroupPage=yes
InfoBeforeFile="SOURCEFILES\Info.rtf"
OutputBaseFilename=SetupSeminarverwaltung80
OutputDir=Output
RestartIfNeededByRun="yes"
SetupIconFile=SOURCEFILES\OLAdmin.ico
SetupLogging="yes"
SolidCompression=yes
;UninstallDisplayIcon={app}\Shared\OLAdmin.ico
UninstallDisplayIcon=Files\OLAdmin.ico
; Uninstallable=no
VersionInfoCompany="Sage GmbH"
VersionInfoCopyright="Copyright (C) 1995-2017 Sage GmbH. Alle Rechte vorbehalten."
VersionInfoDescription="Setup für die Seminarverwaltung der Sage 100 / Office Line"
VersionInfoVersion="8.0.1.0"
WizardImageBackColor="$FFFFFF"
WizardImageFile="SOURCEFILES\Background.bmp"
WizardImageStretch="yes"
WindowResizable="no"
WizardSmallImageFile="SOURCEFILES\Corner.bmp"

[Languages]
Name: "german"; MessagesFile: "compiler:Languages\German.isl"

[Files]
; Programme\Sage\Office Line\5.0\AddIn und Programme\Sage\Office Line\5.0\AddIn\Work
; Source: "FILES\ADDIN\OLAbfSeminarverwaltung71.mda"; DestDir: "{app}\Addin"; Flags: 32bit overwritereadonly ignoreversion
; Source: "FILES\ADDIN\OLAbfSeminarverwaltung71.mde"; DestDir: "{app}\Addin"; Flags: 32bit overwritereadonly ignoreversion
; Source: "FILES\ADDIN\OLAbfSeminarverwaltung71.mde"; DestDir: "{app}\Addin\Work"; Flags: 32bit overwritereadonly ignoreversion

Source: "FILES\SHARED\AutoMapper.dll"; DestDir: "{app}\Shared"; Flags: 32bit overwritereadonly ignoreversion
Source: "FILES\SHARED\EntityFramework.dll"; DestDir: "{app}\Shared"; Flags: 32bit overwritereadonly ignoreversion
Source: "FILES\SHARED\EntityFramework.SqlServer.dll"; DestDir: "{app}\Shared"; Flags: 32bit overwritereadonly ignoreversion

Source: "FILES\SHARED\PSDev.OfficeLine.Academy.BusinessLogic.dll"; DestDir: "{app}\Shared"; Flags: 32bit overwritereadonly ignoreversion
Source: "FILES\SHARED\PSDev.OfficeLine.Academy.DataAccess.dll"; DestDir: "{app}\Shared"; Flags: 32bit overwritereadonly ignoreversion
Source: "FILES\SHARED\PSDev.OfficeLine.Academy.RealTimeData.dll"; DestDir: "{app}\Shared"; Flags: 32bit overwritereadonly ignoreversion
Source: "FILES\SHARED\PSDev.OfficeLine.Academy.SOAP.Contract.dll"; DestDir: "{app}\Shared"; Flags: 32bit overwritereadonly ignoreversion
Source: "FILES\SHARED\PSDev.OfficeLine.Academy.SOAP.Implementation.dll"; DestDir: "{app}\Shared"; Flags: 32bit overwritereadonly ignoreversion
; Source: "FILES\SHARED\OLAbfISeminar.dll"; DestDir: "{app}\Shared"; Flags: 32bit regserver overwritereadonly ignoreversion

Source: "FILES\CAS\PSDSeminarverwaltung.OLKey"; DestDir: "{app}"; Flags: 32bit overwritereadonly ignoreversion
Source: "FILES\METADATA\100096740.Academy.metadata"; DestDir: "{app}\Metadata"; Flags: 32bit overwritereadonly ignoreversion
Source: "FILES\METADATA\100096740.AcademyReporting.metadata"; DestDir: "{app}\Metadata"; Flags: 32bit overwritereadonly ignoreversion

Source: "FILES\DBADMIN\PSDSeminarverwaltung71.upd"; DestDir: "{app}\DBAdmin\Update\90\ReweAbf"; Flags: 32bit overwritereadonly ignoreversion
Source: "FILES\DBADMIN\PSDSeminarverwaltung71.upd"; DestDir: "{app}\DBAdmin\Update\100\ReweAbf"; Flags: 32bit overwritereadonly ignoreversion
Source: "FILES\DBADMIN\PSDSeminarverwaltung71.upd"; DestDir: "{app}\DBAdmin\Update\110\ReweAbf"; Flags: 32bit overwritereadonly ignoreversion
Source: "FILES\DBADMIN\PSDSeminarverwaltung71.upd"; DestDir: "{app}\DBAdmin\Update\120\ReweAbf"; Flags: 32bit overwritereadonly ignoreversion

; Programme\Sage\Office Line\5.0\Shared
; Kopieren der ConfigUpdate-Dateien in das Shared\ConfigUpdates-Verzeichnis
Source: "FILES\CONFIGUPDATE\PSDSeminarverwaltung.configupdate"; DestDir: "{app}\Shared\ConfigUpdates"; Flags: overwritereadonly

[Registry]

[Run]
Filename: "{app}\Shared\Sagede.Shared.UpdateConfigTool.exe"; Parameters: "7.1"
; Filename: "{win}\Microsoft.NET\Framework\v4.0.30319\regasm.exe"; Parameters: """{app}\Shared\PSDev.OfficeLine.Academy.Interop.dll"" /Codebase /silent"
Filename: "{app}\Shared\Sagede.Shared.RealTimeData.Metadata.Exchange.exe"; Parameters: "/action=import /inputfile=Metadata\100096740.Academy.metadata"; Flags: waituntilterminated
Filename: "{app}\Shared\Sagede.Shared.RealTimeData.Metadata.Exchange.exe"; Parameters: "/action=import /inputfile=Metadata\100096740.AcademyReporting.metadata"; Flags: waituntilterminated

[UninstallRun]
Filename: "{app}\Shared\Sagede.Shared.RealTimeData.Metadata.Exchange.exe"; Parameters: "/action=delete /inputfile=Metadata\100096740.Academy.metadata"; Flags: waituntilterminated
Filename: "{app}\Shared\Sagede.Shared.RealTimeData.Metadata.Exchange.exe"; Parameters: "/action=delete /inputfile=Metadata\100096740.AcademyReporting.metadata"; Flags: waituntilterminated


[Code]
// prüft anhand des Vorhandenseins der OL-Config-Datei auf die Existenz der OL
function OLIsInstalled(sVersion: String): Boolean;
begin
  Result:=FileExists(ExpandConstant('{commonappdata}\Sage\Office Line\' + sVersion + '\Office Line.Config'));
end;

// wird zu Beginn des Setups ausgeführt und bricht das Setup ab, wenn die OL nicht installiert ist
function InitializeSetup(): Boolean;
begin
  Result:= True;
  if not OLIsInstalled('8.0') then begin
    Msgbox('Bitte installieren Sie zuerst die Sage 100 / Office Line, bevor Sie die Erweiterungen installieren.', MBError, MB_OK);
    Result:=False;
    exit;
  end;
end;

// ermittelt das Root-Verzeichnis der OL-Installation aus der Config-Datei der übergebenen Version
function GetRootFromXML(sVersion: String): String;
  var sConfigFile : String;
  var oXML : Variant;
  var oNode : Variant;
begin
  sConfigFile:=ExpandConstant('{commonappdata}\Sage\Office Line\' + sVersion + '\Office Line.Config');

  try
    // MS XML-Objekt erzeugen
    oXML:=CreateOleObject('MSXML2.DOMDocument');
    oXML.resolveExternals := false;

    if oXML.load(sConfigFile) then begin
      // Suche den entsprechenden Knoten mit dem Root-Eintrag
      try
        oNode:=oXML.selectSingleNode('/configuration/Office_x0020_Line/add/@name');

        if (oNode.text='Root') then begin
          oNode:=oXML.selectSingleNode('/configuration/Office_x0020_Line/add/@value');
          Result:=oNode.text;
        end else begin
          Result:='';
        end;
      except
        Result:='';
      end;
    end else begin
      Result:='';
    end;
  except
    Result:='';
  end;
end;

// ermittelt das Root-Verzeichnis für DefaultDirName, da dort kein Parameter übergeben werden kann
function GetRoot(sParam: String): String;
  var sTemp : String;
begin
  sTemp:=GetRootFromXML('8.0');

  if sTemp='' then begin
    Result:=ExpandConstant('{pf}\Sage\Sage 100\8.0');
  end else begin
    Result:=GetRootFromXML('8.0');
  end;
end;

