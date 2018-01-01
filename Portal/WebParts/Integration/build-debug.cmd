@echo off
REM nant -buildfile:Integration.debug.build
msbuild Integration.sln
IF ERRORLEVEL 1 pause