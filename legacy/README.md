# mPortal
A custom ASP.NET web content management system (WCMS).

**Dev Login**  
Dev URL: https://localhost:44300/Central/  
Username: admin  
Password: dev123  
Setup URL: https://localhost:44300/Content/Setup.aspx

**Debugging**
Logs
* Log files: ~/Content/Admin/Data/Logs/
* Fallback: ~/App_Data/Logs/

**Deployment**
IIS
* Ensure the Identify Pool has permission to connect to SQL Server
* Ensure the Identify Pool has read/write to file system
* Ensure the Identify Pool can stop/start processes

## Build Prerequisites
- Windows with **.NET Framework 4.8** installed.
- **Visual Studio 2022** (or 2019) including the Build Tools. The build scripts
  call `VsDevCmd.bat` from Visual Studio to provide MSBuild.
- **SQL Server** (Express or LocalDB) where the sample databases can be restored.
- **SQL Server Data Tools 2015** for database projects.

## Initial Setup
1. Clone this repository.
2. Open a command prompt and run `Portal/FIRST-TIME-setup.cmd`.
   This script creates junctions, builds all modules and restores the sample
   database using `DbManager.exe`.
3. If you prefer to build manually, run `Portal/build-debug.cmd` followed by
   `Portal/db-restore.cmd`.
4. Edit the connection strings in the various `*.config` files if your SQL Server
   instance is not `(local)` or `(localdb)\\MSSQLLocalDB`.

## Running the Applications
- The main solutions are located under `Portal/WebSystem/`:
  - `mPortal.sln` – full solution.
- Open the solution in Visual Studio and set the desired web project as the
  startup project.
- For a quick run using IIS Express, execute `Portal/IISExpress - Portal.cmd`
  which launches the site using the configuration under `Portal/Binaries`.

## Database Notes
The repository includes scripts to back up and restore the sample database.
`DbManager.exe` reads its connection string from
`Portal/Binaries/DbManager/DbManager.exe.config`. Adjust this if your SQL Server
instance differs. Logs for database operations are written to `Binaries/Logs`.

## Environment Variables
The build scripts set up the Visual Studio environment automatically via
`Portal/_set-env-vars.cmd`. When running MSBuild manually, call this script
first so that MSBuild and related tools are on the PATH.

## Tests
A small set of unit tests lives in
`Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration.UnitTest`.
Open the test project in Visual Studio and run the tests from the Test Explorer.

## Deployment
Release builds can be produced with `Portal/build-release.cmd` or the
`_build-restoredb-release.cmd` helper which also restores the database.
These commands publish the binaries to `Portal/Binaries` and can be deployed to
IIS. Ensure the application pool identity can connect to the database and has
write access to the content directories as noted above.

## Contributing
1. Create a feature branch for your work.
2. Follow the existing folder structure when adding new projects.
3. Submit a pull request describing the changes.
