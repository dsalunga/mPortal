@echo off
nant -buildfile:Integration.release.build
IF ERRORLEVEL 1 pause