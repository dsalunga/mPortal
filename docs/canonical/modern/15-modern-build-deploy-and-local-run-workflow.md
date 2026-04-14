# 15 - Modern Build Deploy And Local Run Workflow

## Purpose
Define a deterministic, cross-environment build and deployment workflow for the modern platform.

## Target Build/Deploy Model
- Use `GitHub Actions` as the canonical CI/CD target lane for build, test, package, and deployment.
- Current program state: workflows are intentionally disabled in the private repo until release hardening phase.
- Retire NAnt/junction-dependent release mechanics in favor of reproducible artifacts.
- Support local development through documented `dotnet` workflows and optional container profiles.

## Pipeline Requirements
- Separate PR validation, mainline integration, and release pipelines.
- Include quality gates: tests, analyzers, security scans, migration checks.
- Use environment protections/approvals for staging and production promotions.

## Activation Criteria For CI/CD Enablement
- Baseline build/test workflow definitions reviewed and approved.
- Environment protection rules defined (staging/prod approvals and restricted deploy rights).
- Cost guardrails documented for private-repo Actions usage.
- Release rehearsal runbook approved for first staging deployment.

## Developer Experience
- Provide one-command bootstrap for modern host dependencies.
- Keep local run guides aligned with modern app topology and config conventions.
- Publish troubleshooting steps for common migration-era failures.

## Legacy Reference
- [Legacy card baseline](../legacy/15-build-deploy-and-local-run-workflow.md)
