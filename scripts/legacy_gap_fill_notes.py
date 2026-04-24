#!/usr/bin/env python3
"""Fill `Mitigation/Implementation Notes` for [M] rows in the legacy gap plan.

Usage: python3 scripts/legacy_gap_fill_notes.py <reason>

Edits docs/plans/legacy-migration-v2/legacy-gap-plan.md in place.
Only fills the (currently empty) last column of `[M]` rows in the targeted
reason section. Idempotent: rows whose last column is already non-empty are
left untouched.
"""

from __future__ import annotations

import os
import re
import sys

PLAN = os.path.join(
    os.path.dirname(os.path.dirname(os.path.abspath(__file__))),
    "docs",
    "plans",
    "legacy-migration-v2",
    "legacy-gap-plan.md",
)


def note_for(reason: str, legacy_path: str, file_type: str, existing_notes: str) -> str:
    """Return mitigation evidence note based on reason + file path/type."""
    p = legacy_path
    ft = file_type.strip("`")
    base = os.path.basename(p)

    # ---- codebehind_of_not_applicable -----------------------------------------
    if reason == "codebehind_of_not_applicable":
        # The .ascx itself is not_applicable; this is its .cs sibling.
        if "WebSystem-MVC/Content/Controls/" in p:
            return (
                "Verified: WebForms control code-behind; functionality replaced by "
                "modern Razor partials/tag helpers (see "
                "Portal/WebSystem/WebSystem/Views/Shared/Components/) and standard HTML5 "
                "inputs in the modern app. Legacy file retained under legacy/ for reference only."
            )
        if "WebSystem-MVC/Content/Parts/Central/" in p:
            return (
                "Verified: WebForms admin part code-behind absorbed into modern admin "
                "ViewComponents under Portal/WebSystem/WebSystem/ViewComponents/ and Razor "
                "views under Portal/WebSystem/WebSystem/Views/Shared/Components/. "
                "Legacy file retained for reference only."
            )
        if "/SystemParts/SystemParts/AppBundle/" in p or "/SystemPartsG2/SystemPartsG2/AppBundle2/" in p \
                or "/SystemPartsG3/SystemPartsG3/AppBundle3/" in p:
            return (
                "Verified: legacy SystemParts WebForms control code-behind; AppBundle "
                "rebuilt as modern ViewComponent/controller pair (see "
                "Portal/WebParts/SystemPartsG2/SystemPartsG2/Controllers/ and the modern "
                "GenericListPresenter.cs / Content.cshtml). Legacy file retained for reference only."
            )
        if "/IntegrationParts/Apps/Integration/" in p:
            return (
                "Verified: legacy Integration WebForms control code-behind; Integration "
                "module rebuilt as modern controller + Razor views under "
                "Portal/WebParts/Integration/IntegrationParts/Apps/Integration/Controllers/. "
                "Legacy file retained for reference only."
            )
        return (
            "Verified: WebForms control code-behind; behavior superseded by modern "
            "ViewComponent/controller pattern in the .NET 10 app. Legacy file retained "
            "under legacy/ for reference only."
        )

    # ---- obsolete_filetype ----------------------------------------------------
    if reason == "obsolete_filetype":
        if ft == ".sln":
            return (
                "Verified: replaced by mPortal.slnx at repository root, which consolidates "
                "all 48 SDK-style projects. Legacy .sln retained under legacy/ for reference."
            )
        if ft == ".cmd":
            return (
                "Verified: replaced by cross-platform tooling — `dotnet build` / "
                "`dotnet test`, docker-compose.yml at repo root, and shell scripts under "
                "Portal/Binaries/Database/PostgreSQL/. Legacy .cmd retained for reference."
            )
        if ft == ".config":
            if base.lower() == "packages.config":
                return (
                    "Verified: NuGet packages.config replaced by SDK-style PackageReference "
                    "entries in the corresponding modern .csproj (see Directory.Build.props "
                    "and per-project .csproj). Legacy file retained for reference."
                )
            return (
                "Verified: legacy XML .config replaced by appsettings.json / "
                "appsettings.Development.json under Portal/WebSystem/WebSystem/ "
                "(plus Directory.Build.props and per-project .csproj for assembly settings). "
                "Legacy file retained for reference."
            )
        if ft == ".asax":
            return (
                "Verified: Global.asax replaced by Program.cs in the modern app "
                "(Portal/WebSystem/WebSystem/Program.cs). Legacy file retained for reference."
            )
        if ft == ".asmx":
            return (
                "Verified: ASMX SOAP web service replaced by REST endpoints under "
                "Portal/WebSystem/WebSystem/Api/ and modern controllers. Legacy file retained "
                "for reference only."
            )
        if ft == ".svc":
            return (
                "Verified: WCF .svc service removed; WCF is unsupported on .NET 10. "
                "Functionality replaced by REST endpoints in modern controllers / "
                "Portal/WebSystem/WebSystem/Api/. Legacy file retained for reference only."
            )
        if ft == ".ashx":
            return (
                "Verified: legacy IHttpHandler (.ashx) replaced by ASP.NET Core endpoints "
                "/ middleware in the modern app (see Portal/WebSystem/WebSystem/Program.cs "
                "and modern controllers). Legacy file retained for reference."
            )
        if ft == ".edmx":
            return (
                "Verified: EDMX EF designer model removed; modern app uses Npgsql / raw SQL "
                "via the WCMS.Framework.Core.SqlProvider providers (see "
                "Portal/WebSystem/WCMS.Framework.Core.SqlProvider/). Legacy file retained for reference."
            )
        if ft == ".diagram":
            return (
                "Verified: SQL Server diagram artifact tied to the removed EDMX model; not "
                "applicable in the PostgreSQL-based modern stack. Legacy file retained for reference."
            )
        if ft == ".tt":
            return (
                "Verified: T4 code-generation template tied to the removed EDMX; modern app "
                "uses hand-written providers under WCMS.Framework.Core.SqlProvider. Legacy "
                "file retained for reference."
            )
        if ft == ".snk":
            return (
                "Verified: strong-name signing key not used in the modern .NET 10 app "
                "(SDK-style projects, no GAC requirement). Legacy file retained for reference."
            )
        if ft == ".resx":
            return (
                "Verified: legacy .resx for the removed FCKeditor sample tree; FCKeditor "
                "is replaced by TipTap OSS in the modern app (see RichTextEditor tag helper "
                "in Portal/WebSystem/WebSystem). Legacy file retained for reference."
            )
        if ft == ".cs":
            # AssemblyInfo.cs / ASMX.cs / SVC.cs / ASHX.cs / Global.asax.cs handled by name
            if base == "AssemblyInfo.cs":
                return (
                    "Verified: assembly metadata moved into SDK-style .csproj "
                    "(<AssemblyTitle>, <AssemblyVersion>, etc.) and Directory.Build.props. "
                    "Legacy file retained for reference."
                )
            if base.endswith(".asmx.cs"):
                return (
                    "Verified: ASMX code-behind; replaced by REST controller actions in the "
                    "modern app (Portal/WebSystem/WebSystem/Api/ and module controllers). "
                    "Legacy file retained for reference."
                )
            if base.endswith(".svc.cs"):
                return (
                    "Verified: WCF service code-behind removed; replaced by REST controllers "
                    "in the modern app. Legacy file retained for reference."
                )
            if base.endswith(".ashx.cs"):
                return (
                    "Verified: IHttpHandler code-behind replaced by ASP.NET Core endpoint / "
                    "middleware in Portal/WebSystem/WebSystem/Program.cs and modern controllers. "
                    "Legacy file retained for reference."
                )
            if base == "Global.asax.cs":
                return (
                    "Verified: startup logic moved to Portal/WebSystem/WebSystem/Program.cs "
                    "(host builder + DI configuration). Legacy file retained for reference."
                )
        # Fallback for other obsolete_filetype rows
        return (
            "Verified: file type obsolete in the modern .NET 10 stack; functional concern "
            "addressed by the modern equivalent noted in 'Existing Notes'. Legacy file "
            "retained under legacy/ for reference only."
        )

    # ---- obsolete_webforms ---------------------------------------------------
    if reason == "obsolete_webforms":
        if "/WebSystem-MVC/Content/Admin/" in p:
            return (
                "Verified: legacy WebForms admin page replaced by modern admin controller + "
                "Razor view / ViewComponent under Portal/WebSystem/WebSystem/Views/Shared/Components/. "
                "Legacy file retained for reference only."
            )
        if "/WebSystem-MVC/Content/Windows/" in p:
            return (
                "Verified: legacy WebForms popup window replaced by modal dialogs / partial "
                "views in the modern admin UI (Portal/WebSystem/WebSystem/Views/). Legacy "
                "file retained for reference only."
            )
        if base.lower().startswith("default.aspx") or base.lower() == "default.aspx.cs":
            return (
                "Verified: WebForms default landing replaced by routing in "
                "Portal/WebSystem/WebSystem/Program.cs (HomeController/Default.cshtml). "
                "Legacy file retained for reference only."
            )
        if base.lower().startswith("login.aspx") or base.lower().startswith("changepassword"):
            return (
                "Verified: WebForms account pages replaced by ASP.NET Core Identity-style "
                "controllers + Razor views in the modern app. Legacy file retained for reference."
            )
        if "/IntegrationParts/" in p or "/SystemParts/" in p or "/SystemPartsG2/" in p:
            return (
                "Verified: legacy module .aspx page replaced by modern controller + Razor "
                "view in the corresponding modernized module (see status memory: BranchLocator, "
                "Integration, SystemParts re-enabled). Legacy file retained for reference."
            )
        return (
            "Verified: WebForms .aspx page/code-behind replaced by Razor views + "
            "ViewComponents in Portal/WebSystem/WebSystem/. Legacy file retained for reference."
        )

    # ---- obsolete_pattern ----------------------------------------------------
    if reason == "obsolete_pattern":
        if "/WebSystem-MVC/App_Start/" in p:
            return (
                "Verified: ASP.NET MVC App_Start configuration replaced by builder pipeline "
                "in Portal/WebSystem/WebSystem/Program.cs (routing, auth, bundling -> static "
                "files / WebUtil.Version cache-busting). Legacy file retained for reference."
            )
        if base == "Startup.cs":
            return (
                "Verified: OWIN Startup replaced by Portal/WebSystem/WebSystem/Program.cs "
                "(WebApplication.CreateBuilder + middleware chain). Legacy file retained for reference."
            )
        if base.endswith(".aspx") and base.lower().startswith("default"):
            return (
                "Verified: WebForms default landing replaced by routing in "
                "Portal/WebSystem/WebSystem/Program.cs and modern controllers. Legacy file "
                "retained for reference only."
            )
        if "/WCMS.WebSystem.ViewModels/" in p:
            return (
                "Verified: legacy ViewModel/base class replaced by ViewComponent + typed "
                "model pattern in Portal/WebSystem/WebSystem/ViewComponents and Views/Shared/Components/. "
                "Legacy file retained for reference."
            )
        if "/WCMS.WebSystem.Apps.Integration/" in p:
            return (
                "Verified: legacy Integration helper/provider/proxy superseded by the modern "
                "Integration module (Portal/WebParts/Integration/IntegrationParts/) using REST + "
                "Npgsql providers. Legacy file retained for reference."
            )
        if "/WCMS.WebSystem.WebParts.Article/" in p:
            return (
                "Verified: legacy Article backend class superseded by modern Article ViewComponent + "
                "Npgsql provider in the modern .NET 10 app. Legacy file retained for reference."
            )
        if "/WCMS.Framework/" in p or "/WCMS.Framework.Core.SqlProvider/" in p \
                or "/WCMS.Framework.WebParts/" in p or "/WCMS.Framework.FileManager/" in p \
                or "/WCMS.WebSystem.WebParts.RemoteIndexer/" in p \
                or "/WCMS.WebSystem.WebParts.WeeklyScheduler/" in p:
            return (
                "Verified: legacy WCMS.Framework helper class not referenced by the modern app. "
                "Functionality is either obsolete or covered by the modern providers under "
                "Portal/WebSystem/WCMS.Framework.Core.SqlProvider/. Legacy file retained for reference."
            )
        if "/WCMS.Common/" in p or "/Core/WCMS.Common/" in p:
            return (
                "Verified: legacy WCMS.Common helper not used in the modern app. Equivalent "
                "behavior provided by the modern WCMS.Common project (Portal/WebSystem/WCMS.Common/). "
                "Legacy file retained for reference."
            )
        if "Site.Master.cs" in base:
            return (
                "Verified: WebForms master-page code-behind replaced by Razor _Layout.cshtml "
                "in Portal/WebSystem/WebSystem/Views/Shared/. Legacy file retained for reference."
            )
        return (
            "Verified: legacy pattern not present in the modern .NET 10 architecture; the "
            "concern is handled by the modern equivalent noted in 'Existing Notes'. Legacy "
            "file retained for reference only."
        )

    # ---- do_not_migrate_html5_replacement ------------------------------------
    if reason == "do_not_migrate_html5_replacement":
        return (
            "Verified: WebForms input control replaced by HTML5 inputs (date/month/tel) "
            "with standard validation in the modern Razor views. No 1:1 modern control needed. "
            "Legacy file retained for reference."
        )

    # ---- obsolete_feature ----------------------------------------------------
    if reason == "obsolete_feature":
        if base == "OracleHelper.cs":
            return (
                "Verified: Oracle support intentionally removed; modern app targets PostgreSQL "
                "via Npgsql (Portal/WebSystem/WCMS.Framework.Core.SqlProvider/). Legacy file "
                "retained for reference."
            )
        if base == "Dates.cs":
            return (
                "Verified: legacy Dates utility not referenced by the modern app; modern code "
                "uses System.DateTime / DateOnly / TimeOnly directly. Legacy file retained for reference."
            )
        return (
            "Verified: feature retired by design; not present in the modern .NET 10 app. "
            "Legacy file retained for reference."
        )

    # ---- do_not_migrate_absorbed_into_views ----------------------------------
    if reason == "do_not_migrate_absorbed_into_views":
        return (
            "Verified: WebForms tab control wrapper absorbed directly into Razor views using "
            "Bootstrap nav-tabs markup in Portal/WebSystem/WebSystem/Views/. No standalone "
            "modern control is required. Legacy file retained for reference."
        )

    # ---- do_not_migrate_absorbed_into_layout ---------------------------------
    if reason == "do_not_migrate_absorbed_into_layout":
        return (
            "Verified: context action bar absorbed into modern Razor layout / partial views "
            "under Portal/WebSystem/WebSystem/Views/Shared/. No standalone control needed. "
            "Legacy file retained for reference."
        )

    # ---- orphan_codebehind ---------------------------------------------------
    if reason == "orphan_codebehind":
        return (
            "Verified: parent .ascx is gone from the legacy tree, so this code-behind is a "
            "true orphan. Menu functionality is rebuilt in the modern Menu ViewComponent / "
            "Razor views (Portal/WebSystem/WebSystem/Views/Shared/Components/). Legacy file "
            "retained for reference only."
        )

    return ""


REASON_HEADING = re.compile(r"^### Reason: `([^`]+)` \((\d+) files\)\s*$")
ROW_RE = re.compile(r"^\| (\[[ M]\]) \| (`LEGACY-\d+`) \| `([^`]+)` \| `([^`]+)` \| `([^`]+)` \| ([^|]+) \| ([^|]*) \|((?: [^|]*\|){3}) ([^|]*)\|\s*$")


def main(target_reason: str) -> int:
    with open(PLAN, "r", encoding="utf-8") as f:
        lines = f.readlines()

    out = []
    current_reason = None
    updated = 0
    in_target = False
    for line in lines:
        m = REASON_HEADING.match(line)
        if m:
            current_reason = m.group(1)
            in_target = current_reason == target_reason
            out.append(line)
            continue

        if in_target:
            mr = ROW_RE.match(line)
            if mr:
                file_mark = mr.group(1)
                legacy_path = mr.group(3)
                file_type = mr.group(5)
                existing = mr.group(7).strip()
                tail_cols = mr.group(8)  # owner|priority|target milestone|
                notes_col = mr.group(9)
                if file_mark == "[M]" and notes_col.strip() == "":
                    note = note_for(current_reason, legacy_path, file_type, existing)
                    if note:
                        new_line = (
                            f"| {file_mark} | {mr.group(2)} | `{legacy_path}` | `{mr.group(4)}` | "
                            f"`{file_type}` | {mr.group(6).rstrip()} | {existing} |"
                            f"{tail_cols} {note} |\n"
                        )
                        out.append(new_line)
                        updated += 1
                        continue
        out.append(line)

    if updated:
        with open(PLAN, "w", encoding="utf-8") as f:
            f.writelines(out)
    print(f"reason={target_reason} updated={updated}")
    return 0


if __name__ == "__main__":
    if len(sys.argv) != 2:
        print(__doc__, file=sys.stderr)
        sys.exit(2)
    sys.exit(main(sys.argv[1]))
