@echo off

SET mypath=%~dp0
REM echo %mypath:~0,-1%

cd /d %mypath:~0,-1%

REM echo %CMDCMDLINE%

REM call "_IISExpress - SetVars.cmd"
cd ..
set PortalPath=%cd%
cd Portal\Binaries
set PortalBinPath=%cd%
REM pause

if exist "%ProgramFiles(x86)%" (
	REM "_IISExpress - Portal x86.lnk"
	CD /D "%ProgramFiles(x86)%\IIS Express"
) else (
	REM "_IISExpress - Portal.lnk"
	CD /D "%ProgramFiles%\IIS Express"
)

iisexpress.exe /config:%PortalPath%\Portal\Binaries\applicationhost.config /siteid:1
REM cd /d %PortalBinPath%