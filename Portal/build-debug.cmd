
@echo off

REM msbuild project.sln /p:Configuration=Debug
REM echo %CD%
REM pause

call _set-env-vars.cmd

cd WebSystem
call build-debug.cmd
cd ..

cd WebParts/SystemParts
call build-debug.cmd
cd ..\..

cd WebParts/SystemPartsG2
call build-debug.cmd
cd ..\..

cd WebParts/SystemPartsG3
call build-debug.cmd
cd ..\..

cd WebParts/BranchLocator
call build-debug.cmd
cd ..\..

cd WebParts/Integration
call build-debug.cmd
cd ..\..