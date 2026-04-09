# Sensitive Data Findings (For Review)

Generated from tracked-file scans on this branch.

## PII Anonymization Update (2026-04-09)

Completed a full first-party pass over modern and legacy sources for personal identifiers and direct contact points.

Completed:
- Replaced personal email addresses with dummy addresses (primarily `@example.test`) in app templates, seeded XML content, and integration pages.
- Replaced personal contact numbers with dummy values in seed content and UI templates.
- Replaced personal-name social references with neutral placeholders (`Mr. Public Speaker`, `PublicSpeakerOfficial`).
- Replaced legacy image file references tied to personal handles and moved views to neutral asset names (`PublicSpeakerTwitter.jpg`).
- Applied equivalent replacements in both modern (`Portal/...`) and legacy (`legacy/Portal/...`) trees.

Verification snapshot:
- `Bro./Sis./Brother/Sister` salutation scan (first-party scope): `0` findings.
- Known personal identifiers and contact emails from previous findings: `0` findings.
- Real-email domain scan (`gmail/yahoo/me/live/ymail/someorg`) in first-party scope: `0` findings.

Intentional exclusions in this pass:
- Third-party/vendor/plugin payloads and archives (for example plugin bundles and legacy external binary docs).
- SQL logic that references generic domains (for example Gmail normalization checks), because these are behavior rules and not personal identifiers.

## Current HEAD Secret Scan Update (2026-04-09)

Final pass status:
- `gitleaks detect --no-git --source .` => `0` findings in current HEAD.
- Additional remediations completed for previously flagged content:
  - Replaced legacy Google Maps/JS API keys with explicit non-secret placeholders in seeded HTML/XML content.
  - Replaced legacy reCAPTCHA public/private keys with placeholders in `.ascx` sources.
  - Replaced committed IIS `sessionKey` values in `applicationhost.config` snapshots with placeholders.
  - Removed non-essential legacy sample cert file:
    - `legacy/Portal/WebSystem/WebSystem-MVC/Content/Plugins/fckeditor/_samples/adobeair/sample01_cert.pfx`

## Confirmed Sensitive/High-Risk Data (Historical Exposure, Redacted)

1. SMS gateway credential embedded in XML
- File: `Portal/Binaries/Database/WebRegistry.xml:1143`
- Value:
  - `<redacted-sms-gateway-credentialed-url>`
- Legacy duplicate:
  - `legacy/Portal/Binaries/Database/WebRegistry.xml:1143`

2. Hardcoded encryption key and IV in source
- File: `Portal/WebSystem/WCMS.Framework/Utilities/LoginSecurity.cs:101`
- Value:
  - `SECRET_KEY = "<redacted>"`
- File: `Portal/WebSystem/WCMS.Framework/Utilities/LoginSecurity.cs:102`
- Value:
  - `INIT_VECTOR = "<redacted>"`
- Legacy duplicates:
  - `legacy/Portal/WebSystem/WCMS.Framework/Utilities/LoginSecurity.cs:108`
  - `legacy/Portal/WebSystem/WCMS.Framework/Utilities/LoginSecurity.cs:109`

3. Hardcoded password salt constant in source
- File: `Portal/WebSystem/WCMS.Framework/Utilities/SecurityHelper.cs:12`
- Value:
  - `SALT = "<redacted>"`
- Legacy duplicate:
  - `legacy/Portal/WebSystem/WCMS.Framework/Utilities/SecurityHelper.cs:12`

4. Hardcoded fallback DB password in runtime code
- File: `Portal/WebSystem/WebSystem/Program.cs:87`
- Value:
  - `<hardcoded-local-postgres-credential-fallback>`

5. Default weak passwords in compose/runtime env defaults
- File: `docker-compose.yml:12`
- Value:
  - `<weak-password-default-fallback>`
- File: `docker-compose.yml:31`
- Value:
  - `<weak-password-default-fallback>`
- File: `docker-compose.yml:53`
- Value:
  - `ConnectionStrings__DefaultConnection` includes password fallback
- File: `docker-compose.yml:54`
- Value:
  - `ConnectionStrings__ConnectionString` includes password fallback

6. Deployment doc includes default password example
- File: `docs/DEPLOYMENT_RUNBOOK.md:22`
- Value:
  - `<hardcoded-default-password-example>`

Current-branch remediation status (2026-04-09):
- Items 1-6 were removed or replaced with secure placeholders/config-driven values in HEAD.
- Secret rotation and history rewrite are still required to close exposure risk.

## Medium-Risk / Contextual Findings

1. Parameterized password placeholders (not concrete credential by themselves)
- `Portal/Utilities/MySQL TableEditor/Form1.cs:288`
- `legacy/Portal/Utilities/MySQL TableEditor/Form1.cs:288`

2. Placeholder sample credential text
- `legacy/Portal/WebSystem/WebSystem-MVC/Content/Plugins/fckeditor/editor/filemanager/connectors/lasso/config.lasso:33`
- Value contains `password='xxxxxxxx'`

## Proprietary/Internal Asset Exposure Surface

Large number of binary/vendor archives and executables are committed, especially under `legacy/` and `Portal/Binaries/`.
Examples include:
- removed during current-branch remediation: `legacy/Portal/WebSystem/WebSystem-MVC/Content/Plugins/fckeditor/_samples/adobeair/sample01_cert.pfx`
- `legacy/Portal/WebSystem/WebSystem-MVC/Content/Plugins/WebExtractor/WebExtractor.exe`
- `legacy/Portal/WebSystem/WebSystem-MVC/Content/Plugins/WebExtractor/*.dll`
- multiple `.zip`, `.rar`, `.7z`, `.exe` assets under `legacy/` and `Portal/Binaries/`

These are not always secrets, but they may contain proprietary/internal materials and should be reviewed before distribution.

## Git History Evidence (Indicators)

Pattern searches indicate secret-bearing content exists in history (not only current HEAD), including:
- `git log --all -S"<redacted-sms-password-token>"` shows commits including `784a9fcf`, `6fdbf267`
- `git log --all -S"SECRET_KEY"` shows commits including `784a9fcf`, `7a158158`, `28000fc2`, `8ce680ed`
- `git log --all -S"SALT ="` shows commits including `784a9fcf`, `8ce680ed`

## Notes

- This report intentionally keeps concrete secret values redacted.
- Secret rotation and git-history rewrite remain mandatory because prior commits still contain historical exposures.
