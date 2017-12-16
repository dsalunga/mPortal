@echo off
nant -buildfile:mPortal.debug.build
IF ERRORLEVEL 1 pause