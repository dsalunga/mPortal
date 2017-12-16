@echo off
nant -buildfile:System-Parts-G2.debug.build
IF ERRORLEVEL 1 pause