@echo off
nant -buildfile:debug.build

IF ERRORLEVEL 1 pause