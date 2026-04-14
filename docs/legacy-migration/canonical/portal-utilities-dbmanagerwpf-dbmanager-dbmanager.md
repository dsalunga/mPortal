# P008 - DbManager

## Project Tracking Summary

| Field | Value |
|---|---|
| Project Path | `legacy/Portal/Utilities/DbManagerWPF/DbManager/DbManager.csproj` |
| Project Kind | Utility Project |
| Assembly Name | `DbManager` |
| Target Framework | `v4.0` |
| Output Type | `WinExe` |
| Migration Status | Do Not Migrate As-Is |
| Status Basis | WPF shell has minimal operational behavior; replace only if a validated modern tooling need exists. |
| Target Alternative | Retire legacy utility; if needed, replace with lightweight web/CLI ops tooling. |
| Tracking Owner | `TBD` |
| Target Milestone | `TBD` |

## Surface Coverage Snapshot

| Surface | Count |
|---|---:|
| User Control/UI | 2 |
| Core Component | 2 |
| Configuration/Resource | 1 |
| Generated UI Partial | 2 |
| Assembly Metadata | 1 |

## User Controls And UI Components

| Component Type | Feature/Area | Functionality | Migration Status | Target Alternative | Tracking Notes | Source File |
| --- | --- | --- | --- | --- | --- | --- |
| User Control/UI | `(root)` | `App` | Do Not Migrate As-Is | Retire legacy utility; if needed, replace with lightweight web/CLI ops tooling. | Legacy utility shell; do not lift-and-shift. | `App.xaml` |
| User Control/UI | `(root)` | `MainWindow` | Do Not Migrate As-Is | Retire legacy utility; if needed, replace with lightweight web/CLI ops tooling. | Legacy utility shell; do not lift-and-shift. | `MainWindow.xaml` |
| Generated UI Partial | `Properties` | `Resources.Designer` | Do Not Migrate As-Is | Retire legacy utility; if needed, replace with lightweight web/CLI ops tooling. | Legacy utility shell; do not lift-and-shift. | `Properties/Resources.Designer.cs` |
| Generated UI Partial | `Properties` | `Settings.Designer` | Do Not Migrate As-Is | Retire legacy utility; if needed, replace with lightweight web/CLI ops tooling. | Legacy utility shell; do not lift-and-shift. | `Properties/Settings.Designer.cs` |

## Core Components And Utilities

| Component Type | Feature/Area | Functionality | Migration Status | Target Alternative | Tracking Notes | Source File |
| --- | --- | --- | --- | --- | --- | --- |
| Core Component | `(root)` | `App.xaml` | Do Not Migrate As-Is | Retire legacy utility; if needed, replace with lightweight web/CLI ops tooling. | Legacy utility shell; do not lift-and-shift. | `App.xaml.cs` |
| Core Component | `(root)` | `MainWindow.xaml` | Do Not Migrate As-Is | Retire legacy utility; if needed, replace with lightweight web/CLI ops tooling. | Legacy utility shell; do not lift-and-shift. | `MainWindow.xaml.cs` |
| Assembly Metadata | `Properties` | `AssemblyInfo` | Do Not Migrate As-Is | Retire legacy utility; if needed, replace with lightweight web/CLI ops tooling. | Legacy utility shell; do not lift-and-shift. | `Properties/AssemblyInfo.cs` |

## Database And Automation Assets

| Component Type | Feature/Area | Functionality | Migration Status | Target Alternative | Tracking Notes | Source File |
| --- | --- | --- | --- | --- | --- | --- |
| Configuration/Resource | `Properties` | `Resources` | Do Not Migrate As-Is | Retire legacy utility; if needed, replace with lightweight web/CLI ops tooling. | Legacy utility shell; do not lift-and-shift. | `Properties/Resources.resx` |

## Migration Action Items

| Action | Priority | Status | Notes |
|---|---|---|---|
| Confirm owner and target architecture for this artifact | High | Not Stated | `TBD` |
| Validate feature-level parity against source inventory tables above | High | Not Stated | `TBD` |
| Update row-level statuses as migration work progresses | Medium | Not Stated | `TBD` |

