# Security Policy

## Secrets and Sensitive Data
- Do not commit plaintext secrets, credentials, API keys, salts, or encryption keys.
- Do not commit real personal data (names, emails, phone numbers, or contact directories).
- Use environment variables or secret managers for runtime secrets.
- Keep `.env` files local only; commit `.env.example` with placeholders.

## Required Controls
- CI runs `gitleaks` secret scanning on pushes and pull requests.
- Contributors should enable the local pre-commit hook:
  - `git config core.hooksPath .githooks`

## Reporting
- If a secret is exposed:
  1. Rotate/revoke immediately.
  2. Remove from current branch.
  3. Perform history cleanup and force-push only during an approved cutover window.
