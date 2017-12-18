@echo off
nant -buildfile:BranchLocator.debug.build
IF ERRORLEVEL 1 pause