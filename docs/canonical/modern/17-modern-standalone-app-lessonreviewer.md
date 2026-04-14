# 17 - Modern Standalone App LessonReviewer

## Purpose
Define modern architecture and migration outcomes for LessonReviewer.

## Target Shape
- Standalone `.NET 10` host with modern API/UI boundaries.
- Playback/review flows supported by explicit service contracts.
- Shared platform concerns (auth, observability, deployment) aligned with program standards.

## Migration Priorities
- Replace legacy handler/endpoints with modern equivalents.
- Stabilize lesson data access and workflow orchestration.
- Add test coverage for playback and admin review paths.

## Success Criteria
- Parity for critical reviewer/admin journeys.
- Operational telemetry and error diagnostics in place.
- Release path integrated into standard CI/CD lanes.

## Legacy Reference
- [Legacy card baseline](../legacy/17-standalone-app-lessonreviewer.md)
