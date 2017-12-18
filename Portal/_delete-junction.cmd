@echo off

cd WebParts/SystemParts/SystemParts
call delete-junction.cmd
cd ..\..\..

cd WebParts/SystemPartsG2/SystemPartsG2
call delete-junction.cmd
cd ..\..\..

cd WebParts/SystemPartsG3/SystemPartsG3
call delete-junction.cmd
cd ..\..\..


cd WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp
call delete-junction.cmd
cd ..\..\..

cd WebParts/IntegrationParts/IntegrationParts
call delete-junction.cmd
cd ..\..\..