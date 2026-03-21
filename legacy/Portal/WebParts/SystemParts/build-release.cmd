@echo off
nant -buildfile:System-Parts.release.build
IF ERRORLEVEL 1 pause