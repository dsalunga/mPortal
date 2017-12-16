@echo off
nant -buildfile:System-Parts.debug.build
IF ERRORLEVEL 1 pause