@echo off

call _create-junction.cmd
call _first-time-build.cmd
call _db-restore-silent.cmd

ECHO SETUP DONE!!!
pause