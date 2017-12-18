@echo off
nant -buildfile:BranchLocator.release.build
IF ERRORLEVEL 1 pause