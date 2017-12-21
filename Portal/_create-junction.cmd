@echo off

cd WebParts/SystemParts/SystemParts
call create-junction.cmd
cd ..\..\..

cd WebParts/SystemPartsG2/SystemPartsG2
call create-junction.cmd
cd ..\..\..

cd WebParts/SystemPartsG3/SystemPartsG3
call create-junction.cmd
cd ..\..\..


cd WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp
call create-junction.cmd
cd ..\..\..

cd WebParts/Integration/IntegrationParts
call create-junction.cmd
cd ..\..\..