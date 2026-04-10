# 17 - Standalone App LessonReviewer

## Scope
`legacy/LessonReviewer` (`LessonReviewer` web app + `LessonReviewer.Core`).

## Feature summary
- Video/audio playback portal for service replay and make-up workflows.
- Dynamic service/date/language selection based on filesystem media inventory.
- Streaming playlist generation via handler endpoints (`Playback.ashx`).
- Admin configuration UI (`/Admin/Manage.aspx`) with login gate (`/Admin/Login.aspx`).

## Architecture
- WebForms host app (.NET Framework 4.8 target).
- Core library manages config lookup, service definitions, and playback helper logic.
- Handlers provide lightweight AJAX keepalive/status and dynamic playlist output.

## Integration behavior
- Default flow can probe portal endpoint (`PortalAjaxHandlerUrl`) and redirect to portal attendance home.
- Session object (`MakeUpServiceSession`) controls bypass and playback behavior flags.

## Security and operations observations
- Admin username/password are stored in plain XML config (`App_Data/Config.xml`).
- Admin access control is session-based in `Global.asax` request-state hook.
- Build scripts call NAnt targets (`LessonReviewer.*.build`) that are missing in repo.

## Evaluation
Strengths:
- Clear single-purpose application boundary with manageable module size.
- Functional playback assembly logic and segmented media support.

Risks:
- Credential and config handling model is weak for production hardening.
- File-system and config-path dependence can break portability.
- Build automation appears partially stale.

## Key anchors
- `legacy/LessonReviewer/LessonReviewer/Default.aspx.cs`
- `legacy/LessonReviewer/LessonReviewer/Handlers/Playback.ashx.cs`
- `legacy/LessonReviewer/LessonReviewer/Admin/Login.aspx.cs`
- `legacy/LessonReviewer/LessonReviewer/Admin/Manage.aspx.cs`
- `legacy/LessonReviewer/LessonReviewer/App_Data/Config.xml`
- `legacy/LessonReviewer/LessonReviewer.Core/PlaybackHelper.cs`
- `legacy/LessonReviewer/build-debug.cmd`

