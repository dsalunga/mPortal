@echo off

call "IISExpress - SetVars.cmd"
if exist "%ProgramFiles(x86)%" ("_IISExpress - Portal x86.lnk") else ("_IISExpress - Portal.lnk")