@echo off
REM nant -buildfile:System-Parts.debug.build
msbuild System-Parts.sln
IF ERRORLEVEL 1 pause