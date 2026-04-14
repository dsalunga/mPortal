# Agent Instructions

This file is the repository-level instruction source for coding agents in this repository. It complements (and does not override) higher-priority platform/system instructions.

## Core Workflow

- Never commit secrets, credentials, or private keys.

## Planning and Delivery

- Any formal plan/proposal must include a concrete checklist of implementation steps.
- Before committing, perform minimum relevant validation/build checks to ensure nothing is broken.
- When working on large work, after finishing a major chunk, automatically commit it, then update the plan before starting the next major chunk.
- If the user asks to implement a plan end-to-end (for example: "implement until completed", "fully implement", "implement the complete plan"), the agent must complete the full implementation in one run and not stop by phase.
- The only valid reasons to pause before full completion are blocking errors or essential clarifications from the user that are required to proceed safely.
- For new docs plan files under `docs/`, do not use `IMPLEMENTATION` in the filename. Use concise names with `PLAN`, `CHECKLIST`, or `CHECKLIST_PLAN` when applicable.
