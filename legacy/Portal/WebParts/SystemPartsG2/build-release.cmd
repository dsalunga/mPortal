@echo off
nant -buildfile:System-Parts-G2.release.build
IF ERRORLEVEL 1 pause