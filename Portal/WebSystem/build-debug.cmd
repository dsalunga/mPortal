@echo off
REM nant -buildfile:mPortal.debug.build
msbuild mPortal.sln
IF ERRORLEVEL 1 pause