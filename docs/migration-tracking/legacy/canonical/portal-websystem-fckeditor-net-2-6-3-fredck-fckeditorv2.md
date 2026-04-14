# P039 - FredCK.FCKeditorV2

## Project Tracking Summary

| Field | Value |
|---|---|
| Project Path | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FredCK.FCKeditorV2.csproj` |
| Project Kind | Web Application |
| Assembly Name | `FredCK.FCKeditorV2` |
| Target Framework | `v4.8` |
| Output Type | `Library` |
| Migration Status | Do Not Migrate As-Is |
| Status Basis | Embedded FCKeditor package should be replaced by a supported editor integration. |
| Target Alternative | Replace legacy editor with supported editor stack (e.g., CKEditor 5/TinyMCE/Tiptap) + server-side HTML sanitization. |
| Tracking Owner | `TBD` |
| Target Milestone | `TBD` |

## Surface Coverage Snapshot

| Surface | Count |
|---|---:|
| Page/View | 11 |
| Core Component | 18 |
| Configuration/Resource | 4 |
| Assembly Metadata | 1 |

## Pages And Views

| Component Type | Feature/Area | Functionality | Migration Status | Target Alternative | Tracking Notes | Source File |
| --- | --- | --- | --- | --- | --- | --- |
| Page/View | `(root)` | `_documentation` | Do Not Migrate As-Is | Replace legacy editor with supported editor stack (e.g., CKEditor 5/TinyMCE/Tiptap) + server-side HTML sanitization. | Legacy editor package; replace with maintained editor stack. | `_documentation.html` |
| Page/View | `_samples/aspx/1.1` | `sample01` | Do Not Migrate As-Is | Replace legacy editor with supported editor stack (e.g., CKEditor 5/TinyMCE/Tiptap) + server-side HTML sanitization. | Legacy editor package; replace with maintained editor stack. | `_samples/aspx/1.1/sample01.aspx` |
| Page/View | `_samples/aspx/1.1` | `sample02` | Do Not Migrate As-Is | Replace legacy editor with supported editor stack (e.g., CKEditor 5/TinyMCE/Tiptap) + server-side HTML sanitization. | Legacy editor package; replace with maintained editor stack. | `_samples/aspx/1.1/sample02.aspx` |
| Page/View | `_samples/aspx/1.1` | `sample03` | Do Not Migrate As-Is | Replace legacy editor with supported editor stack (e.g., CKEditor 5/TinyMCE/Tiptap) + server-side HTML sanitization. | Legacy editor package; replace with maintained editor stack. | `_samples/aspx/1.1/sample03.aspx` |
| Page/View | `_samples/aspx/1.1` | `sample04` | Do Not Migrate As-Is | Replace legacy editor with supported editor stack (e.g., CKEditor 5/TinyMCE/Tiptap) + server-side HTML sanitization. | Legacy editor package; replace with maintained editor stack. | `_samples/aspx/1.1/sample04.aspx` |
| Page/View | `_samples/aspx/2.0` | `sample01` | Do Not Migrate As-Is | Replace legacy editor with supported editor stack (e.g., CKEditor 5/TinyMCE/Tiptap) + server-side HTML sanitization. | Legacy editor package; replace with maintained editor stack. | `_samples/aspx/2.0/sample01.aspx` |
| Page/View | `_samples/aspx/2.0` | `sample02` | Do Not Migrate As-Is | Replace legacy editor with supported editor stack (e.g., CKEditor 5/TinyMCE/Tiptap) + server-side HTML sanitization. | Legacy editor package; replace with maintained editor stack. | `_samples/aspx/2.0/sample02.aspx` |
| Page/View | `_samples/aspx/2.0` | `sample03` | Do Not Migrate As-Is | Replace legacy editor with supported editor stack (e.g., CKEditor 5/TinyMCE/Tiptap) + server-side HTML sanitization. | Legacy editor package; replace with maintained editor stack. | `_samples/aspx/2.0/sample03.aspx` |
| Page/View | `_samples/aspx/2.0` | `sample04` | Do Not Migrate As-Is | Replace legacy editor with supported editor stack (e.g., CKEditor 5/TinyMCE/Tiptap) + server-side HTML sanitization. | Legacy editor package; replace with maintained editor stack. | `_samples/aspx/2.0/sample04.aspx` |
| Page/View | `_samples/aspx` | `default` | Do Not Migrate As-Is | Replace legacy editor with supported editor stack (e.g., CKEditor 5/TinyMCE/Tiptap) + server-side HTML sanitization. | Legacy editor package; replace with maintained editor stack. | `_samples/aspx/default.html` |
| Page/View | `(root)` | `_whatsnew` | Do Not Migrate As-Is | Replace legacy editor with supported editor stack (e.g., CKEditor 5/TinyMCE/Tiptap) + server-side HTML sanitization. | Legacy editor package; replace with maintained editor stack. | `_whatsnew.html` |

## Core Components And Utilities

| Component Type | Feature/Area | Functionality | Migration Status | Target Alternative | Tracking Notes | Source File |
| --- | --- | --- | --- | --- | --- | --- |
| Assembly Metadata | `(root)` | `AssemblyInfo` | Do Not Migrate As-Is | Replace legacy editor with supported editor stack (e.g., CKEditor 5/TinyMCE/Tiptap) + server-side HTML sanitization. | Legacy editor package; replace with maintained editor stack. | `AssemblyInfo.cs` |
| Core Component | `(root)` | `FCKeditor` | Do Not Migrate As-Is | Replace legacy editor with supported editor stack (e.g., CKEditor 5/TinyMCE/Tiptap) + server-side HTML sanitization. | Legacy editor package; replace with maintained editor stack. | `FCKeditor.cs` |
| Core Component | `(root)` | `FCKeditorConfigurations` | Do Not Migrate As-Is | Replace legacy editor with supported editor stack (e.g., CKEditor 5/TinyMCE/Tiptap) + server-side HTML sanitization. | Legacy editor package; replace with maintained editor stack. | `FCKeditorConfigurations.cs` |
| Core Component | `(root)` | `FCKeditorDesigner` | Do Not Migrate As-Is | Replace legacy editor with supported editor stack (e.g., CKEditor 5/TinyMCE/Tiptap) + server-side HTML sanitization. | Legacy editor package; replace with maintained editor stack. | `FCKeditorDesigner.cs` |
| Core Component | `FileBrowser` | `Config` | Do Not Migrate As-Is | Replace legacy editor with supported editor stack (e.g., CKEditor 5/TinyMCE/Tiptap) + server-side HTML sanitization. | Legacy editor package; replace with maintained editor stack. | `FileBrowser/Config.cs` |
| Core Component | `FileBrowser` | `Connector` | Do Not Migrate As-Is | Replace legacy editor with supported editor stack (e.g., CKEditor 5/TinyMCE/Tiptap) + server-side HTML sanitization. | Legacy editor package; replace with maintained editor stack. | `FileBrowser/Connector.cs` |
| Core Component | `FileBrowser` | `FileWorkerBase` | Do Not Migrate As-Is | Replace legacy editor with supported editor stack (e.g., CKEditor 5/TinyMCE/Tiptap) + server-side HTML sanitization. | Legacy editor package; replace with maintained editor stack. | `FileBrowser/FileWorkerBase.cs` |
| Core Component | `FileBrowser` | `TypeConfig` | Do Not Migrate As-Is | Replace legacy editor with supported editor stack (e.g., CKEditor 5/TinyMCE/Tiptap) + server-side HTML sanitization. | Legacy editor package; replace with maintained editor stack. | `FileBrowser/TypeConfig.cs` |
| Core Component | `FileBrowser` | `TypeConfigList` | Do Not Migrate As-Is | Replace legacy editor with supported editor stack (e.g., CKEditor 5/TinyMCE/Tiptap) + server-side HTML sanitization. | Legacy editor package; replace with maintained editor stack. | `FileBrowser/TypeConfigList.cs` |
| Core Component | `FileBrowser` | `Uploader` | Do Not Migrate As-Is | Replace legacy editor with supported editor stack (e.g., CKEditor 5/TinyMCE/Tiptap) + server-side HTML sanitization. | Legacy editor package; replace with maintained editor stack. | `FileBrowser/Uploader.cs` |
| Core Component | `FileBrowser` | `XmlResponseHandler` | Do Not Migrate As-Is | Replace legacy editor with supported editor stack (e.g., CKEditor 5/TinyMCE/Tiptap) + server-side HTML sanitization. | Legacy editor package; replace with maintained editor stack. | `FileBrowser/XmlResponseHandler.cs` |
| Core Component | `(root)` | `Util` | Do Not Migrate As-Is | Replace legacy editor with supported editor stack (e.g., CKEditor 5/TinyMCE/Tiptap) + server-side HTML sanitization. | Legacy editor package; replace with maintained editor stack. | `Util.cs` |
| Core Component | `(root)` | `XmlUtil` | Do Not Migrate As-Is | Replace legacy editor with supported editor stack (e.g., CKEditor 5/TinyMCE/Tiptap) + server-side HTML sanitization. | Legacy editor package; replace with maintained editor stack. | `XmlUtil.cs` |
| Core Component | `_samples/aspx/1.1` | `sample02.aspx` | Do Not Migrate As-Is | Replace legacy editor with supported editor stack (e.g., CKEditor 5/TinyMCE/Tiptap) + server-side HTML sanitization. | Legacy editor package; replace with maintained editor stack. | `_samples/aspx/1.1/sample02.aspx.cs` |
| Core Component | `_samples/aspx/1.1` | `sample03.aspx` | Do Not Migrate As-Is | Replace legacy editor with supported editor stack (e.g., CKEditor 5/TinyMCE/Tiptap) + server-side HTML sanitization. | Legacy editor package; replace with maintained editor stack. | `_samples/aspx/1.1/sample03.aspx.cs` |
| Core Component | `_samples/aspx/1.1` | `sample04.aspx` | Do Not Migrate As-Is | Replace legacy editor with supported editor stack (e.g., CKEditor 5/TinyMCE/Tiptap) + server-side HTML sanitization. | Legacy editor package; replace with maintained editor stack. | `_samples/aspx/1.1/sample04.aspx.cs` |
| Core Component | `_samples/aspx/2.0` | `sample02.aspx` | Do Not Migrate As-Is | Replace legacy editor with supported editor stack (e.g., CKEditor 5/TinyMCE/Tiptap) + server-side HTML sanitization. | Legacy editor package; replace with maintained editor stack. | `_samples/aspx/2.0/sample02.aspx.cs` |
| Core Component | `_samples/aspx/2.0` | `sample03.aspx` | Do Not Migrate As-Is | Replace legacy editor with supported editor stack (e.g., CKEditor 5/TinyMCE/Tiptap) + server-side HTML sanitization. | Legacy editor package; replace with maintained editor stack. | `_samples/aspx/2.0/sample03.aspx.cs` |
| Core Component | `_samples/aspx/2.0` | `sample04.aspx` | Do Not Migrate As-Is | Replace legacy editor with supported editor stack (e.g., CKEditor 5/TinyMCE/Tiptap) + server-side HTML sanitization. | Legacy editor package; replace with maintained editor stack. | `_samples/aspx/2.0/sample04.aspx.cs` |

## Database And Automation Assets

| Component Type | Feature/Area | Functionality | Migration Status | Target Alternative | Tracking Notes | Source File |
| --- | --- | --- | --- | --- | --- | --- |
| Configuration/Resource | `(root)` | `App` | Do Not Migrate As-Is | Replace legacy editor with supported editor stack (e.g., CKEditor 5/TinyMCE/Tiptap) + server-side HTML sanitization. | Legacy editor package; replace with maintained editor stack. | `App.config` |
| Configuration/Resource | `_samples/aspx/1.1` | `sample02.aspx` | Do Not Migrate As-Is | Replace legacy editor with supported editor stack (e.g., CKEditor 5/TinyMCE/Tiptap) + server-side HTML sanitization. | Legacy editor package; replace with maintained editor stack. | `_samples/aspx/1.1/sample02.aspx.resx` |
| Configuration/Resource | `_samples/aspx/1.1` | `sample03.aspx` | Do Not Migrate As-Is | Replace legacy editor with supported editor stack (e.g., CKEditor 5/TinyMCE/Tiptap) + server-side HTML sanitization. | Legacy editor package; replace with maintained editor stack. | `_samples/aspx/1.1/sample03.aspx.resx` |
| Configuration/Resource | `(root)` | `packages` | Do Not Migrate As-Is | Replace legacy editor with supported editor stack (e.g., CKEditor 5/TinyMCE/Tiptap) + server-side HTML sanitization. | Legacy editor package; replace with maintained editor stack. | `packages.config` |

## Migration Action Items

| Action | Priority | Status | Notes |
|---|---|---|---|
| Confirm owner and target architecture for this artifact | High | Not Stated | `TBD` |
| Validate feature-level parity against source inventory tables above | High | Not Stated | `TBD` |
| Update row-level statuses as migration work progresses | Medium | Not Stated | `TBD` |

