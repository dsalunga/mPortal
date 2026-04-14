# 12 - Branch Locator Module

## Scope
`legacy/Portal/WebParts/BranchLocator`.

## Feature summary
- Maintains hierarchical chapter/branch records (`MChapter`) with geolocation and access flags.
- Provides admin management controls for chapter trees and details.
- Exposes API endpoint(s) for locale announcement availability checks.
- Includes helper utilities for breadcrumb generation, access checks, and distance calculations.

## Data model highlights
`MChapter` tracks:
- parent/child hierarchy
- chapter type (regular/district/division)
- latitude/longitude
- public/internal/restricted access type
- locale bindings and extra metadata payloads

## API highlight
`ChapterController` includes `GET api/v1/Chapter/HasAnnouncements/{LocaleId}` to evaluate global/locale announcement state and last update timestamp.

## Key anchors
- `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator/MChapterSqlProvider.cs`
- `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator/FALHelper.cs`
- `legacy/Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/BranchLocator/Controllers/ChapterController.cs`

## Evaluation
Focused and relatively bounded module with clear data/service edges; a good candidate for modular extraction with lower blast radius than broader CMS subsystems.
