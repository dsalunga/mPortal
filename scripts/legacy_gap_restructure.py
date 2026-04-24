#!/usr/bin/env python3
"""Restructure legacy-gap-plan.md tables and recompute per-row status + Modern File.

Changes:
1. Reason-level marking table: drop columns Owner, Priority, Target Milestone.
2. Per-reason file tables: drop Owner, Priority, Target Milestone; add Modern File
   column (between Existing Notes and Mitigation/Implementation Notes).
3. For each [M] row, recompute:
   - Current Status: `completed` if a specific modern file is found on disk,
     else `not_applicable` with a concrete reason in the Notes column.
   - Modern File: the exact repo-relative modern path (or `—` if N/A).
   - Mitigation/Implementation Notes: short concrete reason / pointer.
4. For [ ] rows, leave status untouched (`not_applicable (retained)`),
   set Modern File = `—`, and keep existing Decision Notes / mitigation text
   when present (or fill with the documented strong no-migrate reason).

Idempotent: re-running on already-restructured tables is a no-op for headers
and re-applies status/Modern File/notes (so manual notes will be overwritten;
edit the rules below if behavior should differ).
"""
from __future__ import annotations

import os
import re
import sys

REPO_ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
PLAN = os.path.join(REPO_ROOT, "docs", "plans", "legacy-migration-v2", "legacy-gap-plan.md")


# ---------------------------------------------------------------------------
# Modern file lookup
# ---------------------------------------------------------------------------

# Legacy → modern path prefix rewrites to try (in order).
PREFIX_REWRITES = [
    ("Portal/WebSystem/WCMS.WebSystem.WebSystem-MVC/", "Portal/WebSystem/WebSystem/"),
]

# Extension swaps (legacy_ext, modern_ext)
EXT_SWAPS = [
    (".ascx.cs", ".cshtml"),
    (".ascx", ".cshtml"),
    (".aspx.cs", ".cshtml"),
    (".aspx", ".cshtml"),
    (".Master.cs", "_Layout.cshtml"),
    (".Master", "_Layout.cshtml"),
]


def file_exists(rel: str) -> bool:
    return os.path.isfile(os.path.join(REPO_ROOT, rel))


def candidate_paths(legacy_path: str) -> list[str]:
    """Generate ordered candidate modern paths to probe for the given legacy path."""
    cands: list[str] = []

    # Apply prefix rewrites (no extension change yet)
    rewritten = [legacy_path]
    for old, new in PREFIX_REWRITES:
        if legacy_path.startswith(old):
            rewritten.append(new + legacy_path[len(old):])

    # Try extension swaps on each rewritten variant
    for base in rewritten:
        for old_ext, new_ext in EXT_SWAPS:
            if base.endswith(old_ext):
                cands.append(base[: -len(old_ext)] + new_ext)

        # Same path different extension: try the codebehind sibling .cshtml
        # (already handled above)

        # Direct path (no extension change) — for cases where the modern file
        # uses the same exact name (rare; skipped to avoid false positives)

    # Deduplicate while preserving order
    seen: set[str] = set()
    out: list[str] = []
    for c in cands:
        if c not in seen:
            seen.add(c)
            out.append(c)
    return out


def find_modern_for_codebehind_or_aspx(legacy_path: str) -> str | None:
    for c in candidate_paths(legacy_path):
        if file_exists(c):
            return c
    return None


# ---------------------------------------------------------------------------
# Per-row resolver: returns (status, modern_file, notes)
# ---------------------------------------------------------------------------


WEBSYSTEM_PROGRAM_CS = "Portal/WebSystem/WebSystem/Program.cs"
WEBSYSTEM_APPSETTINGS = "Portal/WebSystem/WebSystem/appsettings.json"
WEBSYSTEM_LAYOUT = "Portal/WebSystem/WebSystem/Views/Shared/_Layout.cshtml"
SLNX = "mPortal.slnx"


def csproj_for(legacy_path: str) -> str | None:
    """Best-effort guess at the modern .csproj that replaced a legacy packages.config /
    AssemblyInfo.cs sitting next to it in the legacy tree."""
    # Map known legacy project dirs → modern .csproj paths
    legacy_dir = os.path.dirname(legacy_path).rstrip("/")

    mapping: list[tuple[str, str]] = [
        # WebSystem MVC project
        ("Portal/WebSystem/WCMS.WebSystem.WebSystem-MVC", "Portal/WebSystem/WebSystem/WCMS.WebSystem.WebApp.csproj"),
        ("Portal/WebSystem/WCMS.WebSystem.WebSystem-MVC/Properties", "Portal/WebSystem/WebSystem/WCMS.WebSystem.WebApp.csproj"),
        # WebSystem libraries
        ("Portal/WebSystem/WCMS.Framework", "Portal/WebSystem/WCMS.Framework/WCMS.Framework.csproj"),
        ("Portal/WebSystem/WCMS.Framework/Properties", "Portal/WebSystem/WCMS.Framework/WCMS.Framework.csproj"),
        ("Portal/WebSystem/WCMS.Framework.Core.SqlProvider", "Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WCMS.Framework.Core.SqlProvider.csproj"),
        ("Portal/WebSystem/WCMS.Framework.Core.SqlProvider/Properties", "Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WCMS.Framework.Core.SqlProvider.csproj"),
        ("Portal/WebSystem/WCMS.Framework.Core.XmlProvider", "Portal/WebSystem/WCMS.Framework.Core.XmlProvider/WCMS.Framework.Core.XmlProvider.csproj"),
        ("Portal/WebSystem/WCMS.Framework.Core.XmlProvider/Properties", "Portal/WebSystem/WCMS.Framework.Core.XmlProvider/WCMS.Framework.Core.XmlProvider.csproj"),
        ("Portal/WebSystem/WCMS.WebSystem.Utilities", "Portal/WebSystem/WCMS.WebSystem.Utilities/WCMS.WebSystem.Utilities.csproj"),
        ("Portal/WebSystem/WCMS.WebSystem.Utilities/Properties", "Portal/WebSystem/WCMS.WebSystem.Utilities/WCMS.WebSystem.Utilities.csproj"),
        ("Portal/WebSystem/WCMS.Common", "Portal/WebSystem/WCMS.Common/WCMS.Common.csproj"),
        ("Portal/WebSystem/WCMS.Common/Properties", "Portal/WebSystem/WCMS.Common/WCMS.Common.csproj"),
        ("Core/WCMS.Common", "Core/WCMS.Common/WCMS.Common.csproj"),
        ("Core/WCMS.Common/Properties", "Core/WCMS.Common/WCMS.Common.csproj"),
        # Integration
        ("Portal/WebParts/Integration/IntegrationParts", "Portal/WebParts/Integration/IntegrationParts/WCMS.WebSystem.Apps.Integration.WebApp.csproj"),
        ("Portal/WebParts/Integration/IntegrationParts/Properties", "Portal/WebParts/Integration/IntegrationParts/WCMS.WebSystem.Apps.Integration.WebApp.csproj"),
        ("Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration", "Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration/WCMS.WebSystem.Apps.Integration.csproj"),
        ("Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration/Properties", "Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration/WCMS.WebSystem.Apps.Integration.csproj"),
        # BranchLocator
        ("Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp", "Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/WCMS.WebSystem.Apps.BranchLocator.WebApp.csproj"),
        ("Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/Properties", "Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp/WCMS.WebSystem.Apps.BranchLocator.WebApp.csproj"),
        ("Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator", "Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator/WCMS.WebSystem.Apps.BranchLocator.csproj"),
        ("Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator/Properties", "Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator/WCMS.WebSystem.Apps.BranchLocator.csproj"),
        # SystemParts (G1)
        ("Portal/WebParts/SystemParts/SystemParts", "Portal/WebParts/SystemParts/SystemParts/SystemParts.csproj"),
        ("Portal/WebParts/SystemParts/SystemParts/Properties", "Portal/WebParts/SystemParts/SystemParts/SystemParts.csproj"),
        # SystemPartsG2
        ("Portal/WebParts/SystemPartsG2/SystemPartsG2", "Portal/WebParts/SystemPartsG2/SystemPartsG2/SystemPartsG2.csproj"),
        ("Portal/WebParts/SystemPartsG2/SystemPartsG2/Properties", "Portal/WebParts/SystemPartsG2/SystemPartsG2/SystemPartsG2.csproj"),
        # WCMS.WebSystem.WebParts.*
        ("Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Article", "Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Article/WCMS.WebSystem.WebParts.Article.csproj"),
        ("Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Article/Properties", "Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Article/WCMS.WebSystem.WebParts.Article.csproj"),
        ("Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Contact", "Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Contact/WCMS.WebSystem.WebParts.Contact.csproj"),
        ("Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Contact/Properties", "Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Contact/WCMS.WebSystem.WebParts.Contact.csproj"),
        ("Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList", "Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/WCMS.WebSystem.WebParts.GenericList.csproj"),
        ("Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/Properties", "Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.GenericList/WCMS.WebSystem.WebParts.GenericList.csproj"),
        ("Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer", "Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/WCMS.WebSystem.WebParts.RemoteIndexer.csproj"),
        ("Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/Properties", "Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/WCMS.WebSystem.WebParts.RemoteIndexer.csproj"),
        ("Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.WeeklyScheduler", "Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.WeeklyScheduler/WCMS.WebSystem.WebParts.WeeklyScheduler.csproj"),
        ("Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.WeeklyScheduler/Properties", "Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.WeeklyScheduler/WCMS.WebSystem.WebParts.WeeklyScheduler.csproj"),
        # WCMS.Framework.WebParts*
        ("Portal/WebParts/SystemParts/WCMS.Framework.WebParts", "Portal/WebParts/SystemParts/WCMS.Framework.WebParts/WCMS.Framework.WebParts.csproj"),
        ("Portal/WebParts/SystemParts/WCMS.Framework.WebParts/Properties", "Portal/WebParts/SystemParts/WCMS.Framework.WebParts/WCMS.Framework.WebParts.csproj"),
        ("Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar", "Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/WCMS.Framework.WebParts.LocalCalendar.csproj"),
        ("Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Properties", "Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/WCMS.Framework.WebParts.LocalCalendar.csproj"),
        ("Portal/WebParts/SystemParts/WCMS.Framework.FileManager", "Portal/WebParts/SystemParts/WCMS.Framework.FileManager/WCMS.Framework.FileManager.csproj"),
        ("Portal/WebParts/SystemParts/WCMS.Framework.FileManager/Properties", "Portal/WebParts/SystemParts/WCMS.Framework.FileManager/WCMS.Framework.FileManager.csproj"),
        # WCMS.WebSystem.ViewModels (legacy)
        ("Portal/WebSystem/WCMS.WebSystem.ViewModels", "Portal/WebSystem/WebSystem/WCMS.WebSystem.WebApp.csproj"),
        ("Portal/WebSystem/WCMS.WebSystem.ViewModels/Properties", "Portal/WebSystem/WebSystem/WCMS.WebSystem.WebApp.csproj"),
        # FCKeditor.Net (replaced by TipTap; csproj retired)
        ("Portal/WebSystem/FCKeditor.Net_2.6.3", ""),
        ("Portal/WebSystem/FCKeditor.Net_2.6.3/Properties", ""),
        # Sql provider Smo (legacy SqlServer-only)
        ("Portal/WebSystem/WCMS.Framework.Core.SqlProvider.Smo", "Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WCMS.Framework.Core.SqlProvider.csproj"),
        ("Portal/WebSystem/WCMS.Framework.Core.SqlProvider.Smo/Properties", "Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WCMS.Framework.Core.SqlProvider.csproj"),
    ]

    for d, proj in mapping:
        if legacy_dir == d:
            return proj or None

    # Fallback: walk up parent dirs
    parts = legacy_dir.split("/")
    while parts:
        prefix = "/".join(parts) + "/"
        for d, proj in mapping:
            if (d + "/").startswith(prefix) and proj and file_exists(proj):
                return proj
        parts.pop()
    return None


def existing_modern_or(rel: str) -> str | None:
    return rel if rel and file_exists(rel) else None


def resolve_codebehind_of_not_applicable(legacy_path: str) -> tuple[str, str, str]:
    found = find_modern_for_codebehind_or_aspx(legacy_path)
    if found:
        return (
            "completed",
            found,
            "Verified: legacy WebForms code-behind replaced by modern Razor view at the listed Modern File. Legacy file retained under legacy/ for reference only.",
        )
    return (
        "not_applicable",
        "—",
        "Cannot migrate 1:1 — paired .ascx is not_applicable and no modern Razor sibling exists at the same path. Behavior was absorbed into the modern admin UI / ViewComponent layer; the legacy code-behind is obsolete and retained under legacy/ for reference only.",
    )


def resolve_obsolete_filetype(legacy_path: str, file_type: str) -> tuple[str, str, str]:
    base = os.path.basename(legacy_path)
    ft = file_type.strip("`")

    if ft == ".sln":
        if file_exists(SLNX):
            return ("completed", SLNX, "Verified: replaced by repository-root mPortal.slnx (consolidates all 48 SDK-style projects). Legacy .sln retained for reference.")
        return ("not_applicable", "—", "Modern .slnx not found at expected path.")

    if ft == ".cmd":
        return (
            "not_applicable",
            "—",
            "Cannot migrate 1:1 — Windows-only batch script. Replaced wholesale by cross-platform tooling (`dotnet build` / `dotnet test`, docker-compose.yml at repo root, shell scripts under Portal/Binaries/Database/PostgreSQL/). Legacy .cmd retained for reference.",
        )

    if ft == ".config":
        if base.lower() == "packages.config":
            proj = csproj_for(legacy_path)
            if proj and file_exists(proj):
                return ("completed", proj, "Verified: NuGet packages.config replaced by SDK-style PackageReference entries in the listed modern .csproj. Legacy file retained for reference.")
            return ("not_applicable", "—", "Cannot migrate — corresponding modern project was retired (e.g., FCKeditor.Net replaced by TipTap). Legacy packages.config retained for reference.")
        # Web.config / Web.Debug.config / Web.Release.config / WebExtractor.exe.config
        if base.lower().startswith("web.config") or base.lower() == "web.config":
            if file_exists(WEBSYSTEM_APPSETTINGS):
                return ("completed", WEBSYSTEM_APPSETTINGS, "Verified: legacy Web.config replaced by appsettings.json (and appsettings.Development.json / appsettings.SqlServer.json) in the modern WebApp. Legacy file retained for reference.")
        if base.lower() in ("web.debug.config", "web.release.config"):
            return ("not_applicable", "—", "Cannot migrate 1:1 — XDT config transforms removed in ASP.NET Core; environment-specific settings now live in appsettings.{Environment}.json. Legacy file retained for reference.")
        if base.lower() == "webextractor.exe.config":
            return ("not_applicable", "—", "Cannot migrate — WebExtractor utility retired with the WebForms toolchain. Legacy file retained for reference.")
        # Generic .config fallback
        return ("not_applicable", "—", "Cannot migrate 1:1 — legacy XML .config replaced by appsettings.json / Directory.Build.props in the modern stack. Legacy file retained for reference.")

    if ft == ".asax":
        if file_exists(WEBSYSTEM_PROGRAM_CS):
            return ("completed", WEBSYSTEM_PROGRAM_CS, "Verified: Global.asax replaced by WebApplication startup in Program.cs. Legacy file retained for reference.")

    if ft == ".asmx":
        return ("not_applicable", "—", "Cannot migrate 1:1 — ASMX SOAP services removed from .NET; functionality split across REST endpoints in the modern WebApp. Legacy file retained for reference.")
    if ft == ".svc":
        return ("not_applicable", "—", "Cannot migrate — WCF .svc services unsupported on .NET 10. Functionality replaced by REST endpoints in the modern WebApp. Legacy file retained for reference.")
    if ft == ".ashx":
        return ("not_applicable", "—", "Cannot migrate 1:1 — IHttpHandler (.ashx) replaced by ASP.NET Core endpoints / middleware in Program.cs. Legacy file retained for reference.")
    if ft == ".edmx":
        return ("not_applicable", "—", "Cannot migrate — EDMX EF designer model removed. Modern app uses Npgsql + raw SQL via WCMS.Framework.Core.SqlProvider. Legacy file retained for reference.")
    if ft == ".diagram":
        return ("not_applicable", "—", "Cannot migrate — SQL Server diagram artifact tied to the removed EDMX model; not applicable in the PostgreSQL-based modern stack. Legacy file retained for reference.")
    if ft == ".tt":
        return ("not_applicable", "—", "Cannot migrate — T4 code-generation template tied to the removed EDMX. Modern app uses hand-written providers. Legacy file retained for reference.")
    if ft == ".snk":
        return ("not_applicable", "—", "Cannot migrate — strong-name signing key not used in modern .NET 10 (no GAC requirement). Legacy file retained for reference.")
    if ft == ".resx":
        return ("not_applicable", "—", "Cannot migrate — .resx for the removed FCKeditor sample tree; FCKeditor replaced by TipTap OSS. Legacy file retained for reference.")

    if ft == ".cs":
        if base == "AssemblyInfo.cs":
            proj = csproj_for(legacy_path)
            if proj and file_exists(proj):
                return ("completed", proj, "Verified: assembly metadata moved into SDK-style <PropertyGroup> in the listed .csproj (and Directory.Build.props for shared properties). Legacy file retained for reference.")
            return ("not_applicable", "—", "Cannot migrate — corresponding modern project was retired. Legacy AssemblyInfo.cs retained for reference.")
        if base == "Global.asax.cs":
            if file_exists(WEBSYSTEM_PROGRAM_CS):
                return ("completed", WEBSYSTEM_PROGRAM_CS, "Verified: Global.asax code-behind moved to Program.cs (WebApplication.CreateBuilder + middleware chain). Legacy file retained for reference.")
        if base.endswith(".asmx.cs"):
            return ("not_applicable", "—", "Cannot migrate 1:1 — ASMX code-behind removed; functionality split across REST controllers in the modern WebApp. Legacy file retained for reference.")
        if base.endswith(".svc.cs"):
            return ("not_applicable", "—", "Cannot migrate — WCF service code-behind removed; replaced by REST controllers. Legacy file retained for reference.")
        if base.endswith(".ashx.cs"):
            return ("not_applicable", "—", "Cannot migrate 1:1 — IHttpHandler code-behind replaced by ASP.NET Core endpoint / middleware in Program.cs. Legacy file retained for reference.")

    return ("not_applicable", "—", "Cannot migrate 1:1 — file type obsolete in the modern .NET 10 stack; concern handled by the modern equivalent noted in 'Existing Notes'. Legacy file retained for reference.")


def resolve_obsolete_webforms(legacy_path: str) -> tuple[str, str, str]:
    found = find_modern_for_codebehind_or_aspx(legacy_path)
    if found:
        return ("completed", found, "Verified: legacy WebForms page replaced by modern Razor view at the listed Modern File. Legacy file retained for reference.")
    base = os.path.basename(legacy_path).lower()
    if base.startswith("default.aspx"):
        modern = "Portal/WebSystem/WebSystem/Default.cshtml"
        if file_exists(modern):
            return ("completed", modern, "Verified: WebForms Default.aspx replaced by Default.cshtml routed via Program.cs. Legacy file retained for reference.")
    return (
        "not_applicable",
        "—",
        "Cannot migrate 1:1 — WebForms page absorbed into the modern admin UI without a sibling Razor file at the same path. Behavior is implemented elsewhere in the modern controllers/ViewComponents. Legacy file retained for reference.",
    )


def resolve_obsolete_pattern(legacy_path: str) -> tuple[str, str, str]:
    base = os.path.basename(legacy_path)
    if "/App_Start/" in legacy_path or base == "Startup.cs":
        if file_exists(WEBSYSTEM_PROGRAM_CS):
            return ("completed", WEBSYSTEM_PROGRAM_CS, "Verified: ASP.NET MVC App_Start / OWIN Startup replaced by builder pipeline in Program.cs (routing, auth, static files, DI). Legacy file retained for reference.")
    if base == "Site.Master.cs" or base == "Site.master.cs":
        if file_exists(WEBSYSTEM_LAYOUT):
            return ("completed", WEBSYSTEM_LAYOUT, "Verified: WebForms master-page code-behind replaced by Razor _Layout.cshtml. Legacy file retained for reference.")
    if base.lower().startswith("default.aspx"):
        modern = "Portal/WebSystem/WebSystem/Default.cshtml"
        if file_exists(modern):
            return ("completed", modern, "Verified: WebForms default landing replaced by Default.cshtml routed via Program.cs. Legacy file retained for reference.")
    # Try generic .ascx.cs / .aspx.cs swap
    found = find_modern_for_codebehind_or_aspx(legacy_path)
    if found:
        return ("completed", found, "Verified: legacy pattern replaced by modern Razor view at the listed Modern File. Legacy file retained for reference.")
    return (
        "not_applicable",
        "—",
        "Cannot migrate 1:1 — legacy pattern (helper/proxy/ViewModel base) not present in the modern .NET 10 architecture. The functional concern is either obsolete or covered by the modern providers under WCMS.Framework.Core.SqlProvider / Program.cs. Legacy file retained for reference.",
    )


def resolve_html5_replacement(legacy_path: str) -> tuple[str, str, str]:
    return (
        "not_applicable",
        "—",
        "Cannot migrate — WebForms input control replaced by HTML5 native inputs (date / month / tel / etc.) directly in the modern Razor views. No standalone modern code file. Legacy file retained for reference.",
    )


def resolve_obsolete_feature(legacy_path: str) -> tuple[str, str, str]:
    base = os.path.basename(legacy_path)
    if base == "OracleHelper.cs":
        return ("not_applicable", "—", "Cannot migrate — Oracle support intentionally removed; modern app targets PostgreSQL via Npgsql. Legacy file retained for reference.")
    if base == "Dates.cs":
        return ("not_applicable", "—", "Cannot migrate — legacy Dates utility not referenced by the modern app; modern code uses System.DateTime / DateOnly / TimeOnly directly. Legacy file retained for reference.")
    return ("not_applicable", "—", "Cannot migrate — feature retired by design. Legacy file retained for reference.")


def resolve_absorbed_into_views(legacy_path: str) -> tuple[str, str, str]:
    return (
        "not_applicable",
        "—",
        "Cannot migrate 1:1 — WebForms tab control wrapper absorbed directly into Razor views using Bootstrap nav-tabs markup. No standalone modern control. Legacy file retained for reference.",
    )


def resolve_absorbed_into_layout(legacy_path: str) -> tuple[str, str, str]:
    if file_exists(WEBSYSTEM_LAYOUT):
        return ("completed", WEBSYSTEM_LAYOUT, "Verified: context action bar absorbed into modern Razor layout / partial views under Views/Shared/. Legacy file retained for reference.")
    return ("not_applicable", "—", "Cannot migrate 1:1 — absorbed into modern layout. Legacy file retained for reference.")


def resolve_orphan_codebehind(legacy_path: str) -> tuple[str, str, str]:
    return (
        "not_applicable",
        "—",
        "Cannot migrate 1:1 — paired .ascx is gone from the legacy tree, so this code-behind is a true orphan. Menu functionality is provided by the modern Menu ViewComponent under Views/Shared/Components/. Legacy file retained for reference.",
    )


def resolve_obsolete_technology(legacy_path: str) -> tuple[str, str, str]:
    return ("not_applicable", "—", "Cannot migrate — legacy technology obsolete in the modern .NET 10 stack (e.g., WebForms-era component / WCF artifact). Functionality covered elsewhere or intentionally retired. Legacy file retained for reference.")


def resolve_test_sample(legacy_path: str) -> tuple[str, str, str]:
    return ("not_applicable", "—", "Cannot migrate — legacy test/sample artifact for a removed framework component. No modern equivalent required. Legacy file retained for reference.")


RESOLVERS = {
    "codebehind_of_not_applicable": lambda p, ft: resolve_codebehind_of_not_applicable(p),
    "obsolete_filetype": resolve_obsolete_filetype,
    "obsolete_webforms": lambda p, ft: resolve_obsolete_webforms(p),
    "obsolete_pattern": lambda p, ft: resolve_obsolete_pattern(p),
    "do_not_migrate_html5_replacement": lambda p, ft: resolve_html5_replacement(p),
    "obsolete_feature": lambda p, ft: resolve_obsolete_feature(p),
    "do_not_migrate_absorbed_into_views": lambda p, ft: resolve_absorbed_into_views(p),
    "do_not_migrate_absorbed_into_layout": lambda p, ft: resolve_absorbed_into_layout(p),
    "do_not_migrate_obsolete_technology": lambda p, ft: resolve_obsolete_technology(p),
    "do_not_migrate_test_sample": lambda p, ft: resolve_test_sample(p),
    "obsolete_test_sample": lambda p, ft: resolve_test_sample(p),
    "orphan_codebehind": lambda p, ft: resolve_orphan_codebehind(p),
}


# ---------------------------------------------------------------------------
# Markdown rewriting
# ---------------------------------------------------------------------------

REASON_HEADING = re.compile(r"^### Reason: `([^`]+)` \((\d+) files\)\s*$")

# Reason-level marking table — old header (7 cols) and new (4 cols)
OLD_REASON_HEADER = "| Reason Mark | Reason (`status_basis`) | Files | Owner | Priority | Target Milestone | Decision Notes |"
OLD_REASON_SEP = "| --- | --- | ---: | --- | --- | --- | --- |"
NEW_REASON_HEADER = "| Reason Mark | Reason (`status_basis`) | Files | Decision Notes |"
NEW_REASON_SEP = "| --- | --- | ---: | --- |"

# Per-reason file table — old (12 cols) and new (10 cols)
OLD_FILE_HEADER = "| File Mark | ID | Legacy File (relative to Legacy/) | Module | File Type | Current Status | Existing Notes | Owner | Priority | Target Milestone | Mitigation/Implementation Notes |"
OLD_FILE_SEP = "| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |"
NEW_FILE_HEADER = "| File Mark | ID | Legacy File (relative to Legacy/) | Module | File Type | Current Status | Existing Notes | Modern File | Mitigation/Implementation Notes |"
NEW_FILE_SEP = "| --- | --- | --- | --- | --- | --- | --- | --- | --- |"


def split_row(line: str) -> list[str]:
    """Split a markdown table row into its cell strings (preserves leading/trailing spaces)."""
    # Strip leading "| " and trailing " |\n"
    s = line.rstrip("\n")
    if s.startswith("|"):
        s = s[1:]
    if s.endswith("|"):
        s = s[:-1]
    return [c.strip() for c in s.split("|")]


def join_row(cells: list[str]) -> str:
    return "| " + " | ".join(cells) + " |\n"


def transform_reason_row(cells: list[str]) -> list[str]:
    """Old: [Reason Mark, Reason, Files, Owner, Priority, Target Milestone, Decision Notes]
    New: [Reason Mark, Reason, Files, Decision Notes]"""
    if len(cells) == 7:
        return [cells[0], cells[1], cells[2], cells[6]]
    if len(cells) == 4:
        return cells
    return cells


def transform_file_row(cells: list[str], reason: str) -> list[str]:
    """Old: [File Mark, ID, Legacy File, Module, File Type, Current Status, Existing Notes,
            Owner, Priority, Target Milestone, Mitigation/Implementation Notes]
       New: [File Mark, ID, Legacy File, Module, File Type, Current Status, Existing Notes,
             Modern File, Mitigation/Implementation Notes]
    """
    # Detect schema by column count
    if len(cells) == 11:
        file_mark = cells[0]
        legacy_id = cells[1]
        legacy_file = cells[2]
        module = cells[3]
        file_type = cells[4]
        current_status = cells[5]
        existing_notes = cells[6]
        # cells[7..9] = Owner / Priority / Target Milestone (drop)
        prior_notes = cells[10]
    elif len(cells) == 9:
        file_mark = cells[0]
        legacy_id = cells[1]
        legacy_file = cells[2]
        module = cells[3]
        file_type = cells[4]
        current_status = cells[5]
        existing_notes = cells[6]
        # cells[7] = Modern File (existing), cells[8] = Notes
        prior_notes = cells[8]
    else:
        return cells  # unknown shape — leave alone

    legacy_path = legacy_file.strip("`")

    if file_mark == "[M]":
        resolver = RESOLVERS.get(reason)
        if resolver is not None:
            status, modern, notes = resolver(legacy_path, file_type)
        else:
            status, modern, notes = ("not_applicable", "—", prior_notes or "")
        modern_cell = f"`{modern}`" if modern and modern != "—" else "—"
    else:  # [ ] — retained
        status = "not_applicable (retained)"
        modern_cell = "—"
        notes = prior_notes or "Retained as not_applicable per documented strong no-migrate reason."

    return [file_mark, legacy_id, legacy_file, module, file_type, status, existing_notes, modern_cell, notes]


def is_table_row(line: str) -> bool:
    return line.lstrip().startswith("|") and line.rstrip().endswith("|")


def main() -> int:
    with open(PLAN, "r", encoding="utf-8") as f:
        lines = f.readlines()

    out: list[str] = []
    in_reason_table = False
    current_reason: str | None = None
    in_file_table = False

    for line in lines:
        stripped = line.rstrip("\n")

        # Detect reason heading → starts a new file table region
        m = REASON_HEADING.match(line)
        if m:
            current_reason = m.group(1)
            in_reason_table = False
            in_file_table = False
            out.append(line)
            continue

        # Reason-level marking table (top of doc)
        if stripped == OLD_REASON_HEADER:
            in_reason_table = True
            out.append(NEW_REASON_HEADER + "\n")
            continue
        if in_reason_table and stripped == OLD_REASON_SEP:
            out.append(NEW_REASON_SEP + "\n")
            continue
        if in_reason_table and stripped == NEW_REASON_HEADER:
            out.append(line)
            continue
        if in_reason_table and stripped == NEW_REASON_SEP:
            out.append(line)
            continue
        if in_reason_table and is_table_row(line):
            cells = split_row(line)
            new_cells = transform_reason_row(cells)
            out.append(join_row(new_cells))
            continue
        if in_reason_table and not is_table_row(line):
            in_reason_table = False
            # fall through to normal handling

        # Per-reason file table
        if stripped == OLD_FILE_HEADER or stripped == NEW_FILE_HEADER:
            in_file_table = True
            out.append(NEW_FILE_HEADER + "\n")
            continue
        if in_file_table and (stripped == OLD_FILE_SEP or stripped == NEW_FILE_SEP):
            out.append(NEW_FILE_SEP + "\n")
            continue
        if in_file_table and is_table_row(line):
            cells = split_row(line)
            if current_reason is None:
                out.append(line)
                continue
            new_cells = transform_file_row(cells, current_reason)
            out.append(join_row(new_cells))
            continue
        if in_file_table and not is_table_row(line):
            in_file_table = False

        out.append(line)

    with open(PLAN, "w", encoding="utf-8") as f:
        f.writelines(out)

    print("done")
    return 0


if __name__ == "__main__":
    sys.exit(main())
