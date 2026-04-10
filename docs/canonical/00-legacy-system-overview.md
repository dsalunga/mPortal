# 00 - Legacy System Overview

## What this system is
`/legacy` is a large ASP.NET WebForms/MVC WCMS ecosystem centered on `mPortal` and extended by many module packs and side applications.

Core attributes:
- Database-driven CMS behavior (sites, pages, templates, parts, security, registry values all in SQL-backed objects).
- Runtime composition using dynamic control loading and URL rewriting.
- Mixed application styles (WebForms pages/controls, MVC routes, Web API, ASMX, WCF).
- Operational sidecars (agent scheduler, DB manager, deploy/extract utilities, IIS Express scripts).

## Main bounded domains
- `legacy/Portal/WebSystem`: platform core (framework, providers, web host, agent, utilities).
- `legacy/Portal/WebParts`: feature modules (SystemParts G1/G2/G3, Integration, BranchLocator).
- `legacy/BibleReader`: standalone Bible service/UI plus core provider library.
- `legacy/LessonReviewer`: standalone playback/review site and core logic.
- `legacy/Portal/Utilities`: operational desktop/console tools.

## Why it is critical
The platform is tightly coupled: routing, rendering, permissions, and module loading all rely on shared object metadata (`WebObject`) and SQL procedures. Most high-value behavior (CMS editing, security, messaging, integrations) flows through this shared contract.

## High-level evaluation
Strengths:
- Clear extensibility intent through object/provider/manager abstractions.
- Large functional surface already encoded in modules and scripts.
- Built-in operational support (backup/restore, agent jobs, content tooling).

Constraints:
- Heavy static/global state and reflection-based wiring.
- Significant legacy protocol surface and mixed paradigms.
- Sparse automated tests and notable partially implemented paths.
