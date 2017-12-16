@echo off
nant -buildfile:release.build
IF ERRORLEVEL 1 pause