@echo off

REM echo %CD%
REM pause

cd Binaries/DbManager
call DbManager.exe /restore
cd ..\..

pause