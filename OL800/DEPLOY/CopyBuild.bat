DEL "%cd%\FILES\ADDIN\*.*" /Q
DEL "%cd%\FILES\Shared\*.*" /Q
DEL "%cd%\FILES\DBADMIN\*.*" /Q
DEL "%cd%\FILES\METADATA\*.*" /Q
DEL "%cd%\FILES\CAS\*.*" /Q
DEL "%cd%\FILES\CONFIGUPDATE\*.*" /Q

REM XCopy "..\ADDIN\OLAbfSeminarverwaltung71.MDA" "%cd%\FILES\ADDIN\" /K/H/V/C/Q/R
REM XCopy "..\ADDIN\OLAbfSeminarverwaltung71.MDE" "%cd%\FILES\ADDIN\" /K/H/V/C/Q/R

REM "C:\Programme\Microsoft Office\Office15\msaccess.exe" "%cd%\FILES\ADDIN\OLAbfSeminarverwaltung70.MDA" /decompile

XCopy "..\CAS\PSDSeminarverwaltung.OLKey" "%cd%\FILES\CAS\" /K/H/V/C/Q/R

XCopy "..\SKRIPTE\PSDSeminarverwaltung71.upd" "%cd%\FILES\CAS\" /K/H/V/C/Q/R
XCopy "..\CONFIGUPDATE\PSDSeminarverwaltung.configupdate" "%cd%\FILES\CONFIGUPDATE\" /K/H/V/C/Q/R
XCopy "..\METADATA\100096740.Academy.metadata" "%cd%\FILES\METADATA\" /K/H/V/C/Q/R
XCopy "..\METADATA\100096740.AcademyReporting.metadata" "%cd%\FILES\METADATA\" /K/H/V/C/Q/R

XCopy "..\SKRIPTE\PSDSeminarverwaltung71.upd" "%cd%\FILES\DBADMIN\" /K/H/V/C/Q/R

XCopy "C:\Program Files (x86)\Sage\Sage 100\8.0\Shared\PSDev.OfficeLine.Academy.BusinessLogic.dll" "%cd%\FILES\Shared\" /K/H/V/C/Q/R
XCopy "C:\Program Files (x86)\Sage\Sage 100\8.0\Shared\PSDev.OfficeLine.Academy.DataAccess.dll" "%cd%\FILES\Shared\" /K/H/V/C/Q/R
XCopy "C:\Program Files (x86)\Sage\Sage 100\8.0\Shared\PSDev.OfficeLine.Academy.RealTimeData.dll" "%cd%\FILES\Shared\" /K/H/V/C/Q/R
XCopy "C:\Program Files (x86)\Sage\Sage 100\8.0\Shared\PSDev.OfficeLine.Academy.Interop.dll" "%cd%\FILES\Shared\" /K/H/V/C/Q/R
XCopy "C:\Program Files (x86)\Sage\Sage 100\8.0\Shared\PSDev.OfficeLine.Academy.SOAP.Contract.dll" "%cd%\FILES\Shared\" /K/H/V/C/Q/R
XCopy "C:\Program Files (x86)\Sage\Sage 100\8.0\Shared\PSDev.OfficeLine.Academy.SOAP.Implementation.dll" "%cd%\FILES\Shared\" /K/H/V/C/Q/R

XCopy "C:\Program Files (x86)\Sage\Sage 100\8.0\Shared\AutoMapper.dll" "%cd%\FILES\Shared\" /K/H/V/C/Q/R
XCopy "C:\Program Files (x86)\Sage\Sage 100\8.0\Shared\EntityFramework.dll" "%cd%\FILES\Shared\" /K/H/V/C/Q/R
XCopy "C:\Program Files (x86)\Sage\Sage 100\8.0\Shared\EntityFramework.SqlServer.dll" "%cd%\FILES\Shared\" /K/H/V/C/Q/R

REM XCopy "C:\Program Files (x86)\Sage\Sage 100\8.0\Shared\OLAbfISeminar.dll" "%cd%\FILES\Shared\" /K/H/V/C/Q/R

REM "C:\Program Files (x86)\Microsoft Office\Office15\msaccess.exe" "%cd%\FILES\ADDIN\OLAbfSeminarverwaltung71.mda" /CMD PROTECT C:\Projekte\700\CAS\PSDSeminarverwaltung.OLKey Private Key - Nicht weitergeben
REM "C:\Program Files (x86)\Microsoft Office\Office15\msaccess.exe" "%cd%\FILES\ADDIN\OLAbfSeminarverwaltung71.mde" /CMD PROTECT C:\Projekte\700\CAS\PSDSeminarverwaltung.OLKey Private Key - Nicht weitergeben

REM "C:\Program Files (x86)\Microsoft Office\Office15\msaccess.exe" "%cd%\FILES\ADDIN\OLAbfSeminarverwaltung71.mda" /CMD PROTECT C:\Projekte\700\CAS\PSDSeminarverwaltung.OLKey Private Key - Nicht weitergeben


REM TODO: Skript noch berücksichtigen
REM XCopy "..\..\SKRIPTE\XYZUpdate71.upd" "%cd%\FILES\DBAdmin\" /K/H/V/C/Q/R
REM XCopy "..\..\SKRIPTE\XYZRights711.upd" "%cd%\FILES\DBAdmin\" /K/H/V/C/Q/R
REM XCopy "..\..\SKRIPTE\XYZRights712.upd" "%cd%\FILES\DBAdmin\" /K/H/V/C/Q/R
REM XCopy "..\..\SKRIPTE\XYZRights713.upd" "%cd%\FILES\DBAdmin\" /K/H/V/C/Q/R

"C:\Program Files (x86)\Inno Setup 5\compil32.exe" /cc "%cd%\SeminarverwaltungOLClient.iss"

PAUSE
