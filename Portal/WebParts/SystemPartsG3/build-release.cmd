@echo off
nant -buildfile:System-Parts-G3.release.build
IF ERRORLEVEL 1 pause