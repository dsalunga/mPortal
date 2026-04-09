# Security Git History Rewrite Dry-Run Report (2026-04-09)

Status: completed dry-run in disposable mirror clone (no force-push performed)

## Scope
- Source repository: `/Users/dsalunga/Projects/github.com/dsalunga/mPortal`
- Dry-run mirror: `/tmp/mportal-history-rewrite-20260409-114228.git`
- Rules file used: `/tmp/mportal-history-replacements.txt`
- Tools:
  - `git-filter-repo`: `a40bce548d2c`
  - `gitleaks`: `8.30.1`

## Rewrite Inputs
- Literal replacement rules for known historical values:
  - legacy login encryption key and IV
  - legacy password salt
  - legacy SMS URL credential
  - legacy Google Maps / JS API keys
  - legacy reCAPTCHA keys
  - legacy IIS `sessionKey` values
  - local fallback `Password=postgres`
- Path removal rules:
  - `legacy/Portal/WebSystem/WebSystem-MVC/Content/Plugins/fckeditor/_samples/adobeair/sample01_cert.pfx`
  - `Portal/WebSystem/WebSystem-MVC/Content/Plugins/fckeditor/_samples/adobeair/sample01_cert.pfx`

## Secret Scan Results
- Baseline full-history scan (`gitleaks git`): `32` findings
- Post-rewrite full-history scan (`gitleaks git`): `0` findings

Baseline finding distribution:
- Rule IDs:
  - `generic-api-key`: `32`
- File concentration:
  - `Portal/Binaries/Database/WebContent.xml`: `10`
  - `legacy/Portal/Binaries/Database/WebContent.xml`: `10`
  - `Portal/Binaries/Database/WebTextResource.xml`: `1`
  - `legacy/Portal/Binaries/Database/WebTextResource.xml`: `1`
  - `Portal/Binaries/applicationhost.config`: `1`
  - `legacy/Portal/Binaries/applicationhost.config`: `1`
  - `Portal/WebParts/Integration/IntegrationParts/Apps/Contact-Old.htm`: `1`
  - `legacy/Portal/WebParts/Integration/IntegrationParts/Apps/Contact-Old.htm`: `1`
  - `Portal/WebParts/Integration/IntegrationParts/Apps/Integration/Account/GetAccess.ascx`: `1`
  - `legacy/Portal/WebParts/Integration/IntegrationParts/Apps/Integration/Account/GetAccess.ascx`: `1`
  - `Portal/WebParts/SystemParts/SystemParts/AppBundle/Contact/ContactUsV2.ascx`: `1`
  - `legacy/Portal/WebParts/SystemParts/SystemParts/AppBundle/Contact/ContactUsV2.ascx`: `1`
  - `Portal/WebSystem/WCMS.Framework/Utilities/LoginSecurity.cs`: `1`
  - `legacy/Portal/WebSystem/WCMS.Framework/Utilities/LoginSecurity.cs`: `1`

## Ref Impact
- Total refs before: `36`
- Total refs after: `19`
- Changed refs (same name, different commit): `5`
  - `refs/heads/codex/net10-modernization`
  - `refs/heads/copilot/update-net10-migration-tasks`
  - `refs/heads/feat/update-net10-migration-tasks`
  - `refs/heads/local-prep`
  - `refs/heads/master`
- Removed refs: `31`
  - all `refs/remotes/*` tracking refs were removed in rewritten mirror
- Added refs: `14`
  - rewritten `refs/heads/dependabot/...` refs appeared as local heads in mirror

Tag impact:
- Tags before: `0`
- Tags after: `0`
- Changed tags: `0`

## Operational Notes
- This dry-run was executed in an isolated mirror clone and did not modify the working repository history.
- The ref-shape changes (loss of `refs/remotes/*`, addition of local `refs/heads/dependabot/*`) are expected in this style of mirror rewrite and should be normalized in final cutover planning.

## Recommended Next Steps Before Cutover
1. Approve replacement rules and add/remove any additional patterns required by security owner.
2. Approve final rewrite scope (which branches/tags are authoritative for force-push).
3. Publish contributor reset instructions (`re-clone` or `hard reset`) and freeze window.
4. Execute the same validated procedure on canonical repo clone, then force-push rewritten refs.
