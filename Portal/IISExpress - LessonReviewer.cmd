@echo off

REM call "IISExpress - SetVars.cmd"
cd ..
set PortalPath=%cd%
REM cd Portal\Binaries

if exist "%ProgramFiles(x86)%" (
	REM "_IISExpress - Portal x86.lnk"
	CD /D "%ProgramFiles(x86)%\IIS Express"
) else (
	REM "_IISExpress - Portal.lnk"
	CD /D "%ProgramFiles%\IIS Express"
)

iisexpress.exe /config:%PortalPath%\Portal\Binaries\applicationhost.config /siteid:2