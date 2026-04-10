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

| Component Type | Feature/Area | Functionality | Source File | Migration Status | Tracking Notes |
|---|---|---|---|---|---|
| User Control/UI | `(root)` | `App` | `App.xaml` | Do Not Migrate As-Is | Legacy utility shell; do not lift-and-shift. |
| User Control/UI | `(root)` | `MainWindow` | `MainWindow.xaml` | Do Not Migrate As-Is | Legacy utility shell; do not lift-and-shift. |
| Generated UI Partial | `Properties` | `Resources.Designer` | `Properties/Resources.Designer.cs` | Do Not Migrate As-Is | Legacy utility shell; do not lift-and-shift. |
| Generated UI Partial | `Properties` | `Settings.Designer` | `Properties/Settings.Designer.cs` | Do Not Migrate As-Is | Legacy utility shell; do not lift-and-shift. |

## Core Components And Utilities

| Component Type | Feature/Area | Functionality | Source File | Migration Status | Tracking Notes |
|---|---|---|---|---|---|
| Core Component | `(root)` | `App.xaml` | `App.xaml.cs` | Do Not Migrate As-Is | Legacy utility shell; do not lift-and-shift. |
| Core Component | `(root)` | `MainWindow.xaml` | `MainWindow.xaml.cs` | Do Not Migrate As-Is | Legacy utility shell; do not lift-and-shift. |
| Assembly Metadata | `Properties` | `AssemblyInfo` | `Properties/AssemblyInfo.cs` | Do Not Migrate As-Is | Legacy utility shell; do not lift-and-shift. |

## Database And Automation Assets

| Component Type | Feature/Area | Functionality | Source File | Migration Status | Tracking Notes |
|---|---|---|---|---|---|
| Configuration/Resource | `Properties` | `Resources` | `Properties/Resources.resx` | Do Not Migrate As-Is | Legacy utility shell; do not lift-and-shift. |

## Migration Action Items

| Action | Priority | Status | Notes |
|---|---|---|---|
| Confirm owner and target architecture for this artifact | High | Not Stated | `TBD` |
| Validate feature-level parity against source inventory tables above | High | Not Stated | `TBD` |
| Update row-level statuses as migration work progresses | Medium | Not Stated | `TBD` |

