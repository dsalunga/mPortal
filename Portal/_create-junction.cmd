@echo off

cd WebParts/Integration-G2/WCMS.WebSystem.WebParts.Integration.WebAppG2
call create-junction.cmd
cd ..\..\..

cd WebParts/IntegrationParts/IntegrationParts
call create-junction.cmd
cd ..\..\..

cd WebParts/SystemParts/SystemParts
call create-junction.cmd
cd ..\..\..

cd WebParts/SystemPartsG2/SystemPartsG2
call create-junction.cmd
cd ..\..\..

cd WebParts/SystemPartsG3/SystemPartsG3
call create-junction.cmd
cd ..\..\..