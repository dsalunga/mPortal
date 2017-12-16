@echo off
nant -buildfile:mPortal.release.build
IF ERRORLEVEL 1 pause