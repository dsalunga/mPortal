@echo off
REM nant -buildfile:BranchLocator.debug.build
msbuild BranchLocator.sln
IF ERRORLEVEL 1 pause