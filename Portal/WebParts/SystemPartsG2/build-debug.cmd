@echo off
REM nant -buildfile:System-Parts-G2.debug.build
msbuild System-Parts-G2.sln
IF ERRORLEVEL 1 pause