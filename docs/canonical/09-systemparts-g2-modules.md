# 09 - SystemParts G2 Modules

## Scope
`legacy/Portal/WebParts/SystemPartsG2` (second-generation module pack).

## Main feature groups
- Ads and ad redirection flows.
- Media and download content screens.
- Newsletter subscription and campaign controls.
- Social wall model (`WallUpdate`, `WallPlugin`) and view-model bridge.
- Forum domain placeholders (`Forum`, `ForumThread`, `ForumPost`, categories).
- Additional shared UI bundle (`AppBundle2`) with CMS/admin controls.

## Technical notes
- Social module uses framework-style provider/manager pattern and object metadata integration.
- Forum domain classes appear skeletal with unimplemented object id members.
- Pack still uses the same content/bin junction strategy as G1.

## Why it matters
G2 carries communication/community features that extend core CMS beyond content publishing.

## Key anchors
- `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Social/*`
- `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts.Forum/*`
- `legacy/Portal/WebParts/SystemPartsG2/WCMS.WebSystem.WebParts/Newsletter/*`
- `legacy/Portal/WebParts/SystemPartsG2/SystemPartsG2/AppBundle2/*`

## Evaluation
Moderate maturity: social/newsletter are structurally integrated, but forum and selected entities are partially implemented and require hardening before critical reliance.
