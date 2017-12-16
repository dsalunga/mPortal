@echo off
nant -buildfile:System-Parts-G3.debug.build
IF ERRORLEVEL 1 pause