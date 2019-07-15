@echo off

call "IISExpress - SetVars.cmd"
if exist "%ProgramFiles(x86)%" ("_IISExpress - MakeUp x86.lnk") else ("_IISExpress - MakeUp.lnk")