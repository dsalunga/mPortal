@echo off
REM nant -buildfile:System-Parts-G3.debug.build
msbuild System-Parts-G3.sln
IF ERRORLEVEL 1 pause