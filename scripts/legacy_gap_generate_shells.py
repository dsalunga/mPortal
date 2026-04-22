#!/usr/bin/env python3
"""Generate real Razor admin shells for every pending row in legacy-gap-plan.md.

For each row marked `pending | re-migrate (...)` whose Modern File points to an
auto-migrated wrapper, this script:

  1. Locates the wrapper's backing ViewComponent (`{Name}ViewComponent.cs`) and
     `Views/Shared/Components/{Name}/Default.cshtml`.
  2. Parses the corresponding legacy `.ascx` (or `.aspx`) for ASP.NET WebForms
     controls (`<asp:*>`).
  3. Parses the legacy code-behind (`.ascx.cs` / `.aspx.cs`) for namespace and
     event-handler hints.
  4. Rewrites the ViewModel with one strongly-typed property per detected
     control, classifies the page as Edit / List / Mixed, and rewrites the
     `Default.cshtml` with a real Bootstrap admin shell (form fields, lists,
     submit/cancel buttons, status alerts) instead of the
     "Section: Update" placeholder stub.
  5. Wires the POST form to a TODO-marked controller action so the shell is
     functional, compiles, and submits — actual persistence is left as a
     clearly-commented hook.

The output is intentionally a *shell* (Option B "pragmatic working UI"): real
markup matching the legacy structure with TODO-marked service hooks.
"""
from __future__ import annotations

import os
import re
import sys
import unicodedata
from dataclasses import dataclass, field
from pathlib import Path
from typing import Optional

ROOT = Path(__file__).resolve().parents[1]
PLAN = ROOT / "docs/plans/legacy-migration-v2/legacy-gap-plan.md"

CONTROL_RE = re.compile(
    r'<asp:(?P<type>\w+)\b(?P<attrs>[^>]*?)(?:/>|>)',
    re.IGNORECASE,
)
ATTR_RE = re.compile(r'(\w[\w\-:]*)\s*=\s*"([^"]*)"', re.IGNORECASE)
INVOKE_RE = re.compile(r'Component\.InvokeAsync\(\s*"([^"]+)"', re.IGNORECASE)
NAMESPACE_RE = re.compile(r'^\s*namespace\s+([^\s{;]+)', re.MULTILINE)

EDIT_HINTS = ('edit', 'form', 'composer', 'create', 'add', 'config', 'rename', 'setup', 'update', 'newsletter', 'changepassword', 'inquiriesdetails', 'contactdetails', 'fileview', 'texteditor', 'createfolder')
LIST_HINTS = ('list', 'manager', 'browse', 'thumbnails', 'fullview', 'dashboard', 'home', 'results', 'responses', 'view', 'home', 'photos', 'inquirieslist', 'contactlist', 'eventview', 'remotelibraryview', 'remotelibrarymanager')


@dataclass
class Control:
    type: str
    id: str
    label: str
    placeholder: str = ""
    visible: bool = True


@dataclass
class LegacyParts:
    ascx_path: Path
    code_path: Optional[Path]
    namespace: Optional[str]
    controls: list[Control] = field(default_factory=list)
    has_grid: bool = False
    has_repeater: bool = False
    page_kind: str = "edit"  # "edit" | "list" | "mixed"


def humanize(s: str) -> str:
    s = re.sub(r'^(txt|cmd|btn|lbl|ddl|cbx|chk|hf|h|panel|grid|repeater|rpt|gv|lv|view)', '', s, flags=re.IGNORECASE)
    s = re.sub(r'(?<=[a-z])(?=[A-Z])', ' ', s)
    s = s.replace('_', ' ').strip()
    return s.title() if s else "Field"


def classify_kind(filename_stem: str, controls: list[Control], has_grid: bool, has_repeater: bool) -> str:
    name_lc = filename_stem.lower()
    if has_grid or has_repeater:
        # Manager/list pages with editor controls beneath count as mixed
        editor_count = sum(1 for c in controls if c.type.lower() in {'textbox', 'dropdownlist', 'checkbox', 'radiobutton'})
        return "mixed" if editor_count >= 3 else "list"
    for hint in LIST_HINTS:
        if hint in name_lc:
            return "list"
    for hint in EDIT_HINTS:
        if hint in name_lc:
            return "edit"
    return "edit"


def parse_legacy_ascx(ascx: Path, code: Optional[Path]) -> LegacyParts:
    text = ascx.read_text(encoding='utf-8', errors='replace')
    ns = None
    if code and code.exists():
        m = NAMESPACE_RE.search(code.read_text(encoding='utf-8', errors='replace'))
        if m:
            ns = m.group(1)
    controls: list[Control] = []
    seen_ids: set[str] = set()
    has_grid = False
    has_repeater = False
    for cm in CONTROL_RE.finditer(text):
        ctype = cm.group('type')
        ctype_lc = ctype.lower()
        attrs = dict((k.lower(), v) for k, v in ATTR_RE.findall(cm.group('attrs')))
        if ctype_lc in ('view',):
            continue
        if ctype_lc in ('gridview', 'datagrid', 'datalist', 'listview'):
            has_grid = True
        if ctype_lc == 'repeater':
            has_repeater = True
        cid = attrs.get('id') or attrs.get('runat') or ''
        if not cid:
            continue
        if cid in seen_ids:
            continue
        seen_ids.add(cid)
        label = humanize(cid) or cid
        placeholder = attrs.get('placeholder', '')
        visible = attrs.get('visible', 'true').lower() != 'false'
        controls.append(Control(type=ctype, id=cid, label=label, placeholder=placeholder, visible=visible))
    parts = LegacyParts(ascx_path=ascx, code_path=code, namespace=ns, controls=controls,
                        has_grid=has_grid, has_repeater=has_repeater)
    parts.page_kind = classify_kind(ascx.stem, controls, has_grid, has_repeater)
    return parts


# ----- field rendering -----

def field_property(ctype: str) -> tuple[str, str]:
    """Return (csharp_type, default_initializer)."""
    t = ctype.lower()
    if t in ('checkbox',):
        return "bool", "false"
    if t in ('hiddenfield',):
        return "string", "string.Empty"
    if t in ('dropdownlist', 'radiobuttonlist', 'checkboxlist', 'listbox'):
        return "string", "string.Empty"
    if t in ('image',):
        return "string", "string.Empty"
    if t in ('label', 'literal'):
        return "string", "string.Empty"
    return "string", "string.Empty"


def render_field_html(c: Control) -> str:
    ctype = c.type.lower()
    field_id = f"f-{c.id.lower()}"
    safe = c.id
    label = c.label or c.id
    if ctype == 'textbox':
        attrs = c.placeholder and f' placeholder="{c.placeholder}"' or ''
        return (f'                <div class="mb-3">\n'
                f'                    <label class="form-label" for="{field_id}">{label}</label>\n'
                f'                    <input type="text" class="form-control" id="{field_id}" name="{safe}" value="@Model.{safe}"{attrs} />\n'
                f'                </div>')
    if ctype in ('dropdownlist', 'radiobuttonlist', 'checkboxlist', 'listbox'):
        return (f'                <div class="mb-3">\n'
                f'                    <label class="form-label" for="{field_id}">{label}</label>\n'
                f'                    <select class="form-select" id="{field_id}" name="{safe}">\n'
                f'                        <option value="">-- Select --</option>\n'
                f'                        @* TODO: populate options from service *@\n'
                f'                    </select>\n'
                f'                </div>')
    if ctype == 'checkbox':
        return (f'                <div class="form-check mb-3">\n'
                f'                    <input type="checkbox" class="form-check-input" id="{field_id}" name="{safe}" value="true" @(Model.{safe} ? "checked" : null) />\n'
                f'                    <label class="form-check-label" for="{field_id}">{label}</label>\n'
                f'                </div>')
    if ctype == 'hiddenfield':
        return f'                <input type="hidden" name="{safe}" value="@Model.{safe}" />'
    if ctype == 'fileupload':
        return (f'                <div class="mb-3">\n'
                f'                    <label class="form-label" for="{field_id}">{label}</label>\n'
                f'                    <input type="file" class="form-control" id="{field_id}" name="{safe}" />\n'
                f'                </div>')
    if ctype in ('label', 'literal'):
        return (f'                <div class="mb-3">\n'
                f'                    <strong>{label}:</strong>\n'
                f'                    <span>@Model.{safe}</span>\n'
                f'                </div>')
    if ctype in ('button', 'linkbutton', 'imagebutton'):
        # rendered separately as action buttons
        return ''
    if ctype == 'image':
        return (f'                <div class="mb-3">\n'
                f'                    <label class="form-label">{label}</label><br />\n'
                f'                    @if (!string.IsNullOrEmpty(Model.{safe})) {{ <img src="@Model.{safe}" alt="{label}" class="img-fluid" /> }}\n'
                f'                </div>')
    if ctype in ('panel',):
        return ''
    if ctype == 'hyperlink':
        return (f'                <div class="mb-3">\n'
                f'                    <a href="@Model.{safe}">{label}</a>\n'
                f'                </div>')
    return ''


def render_buttons(controls: list[Control]) -> str:
    btn_html = []
    for c in controls:
        if c.type.lower() in ('button', 'linkbutton', 'imagebutton'):
            css = "btn-primary" if any(k in c.id.lower() for k in ('save', 'submit', 'update', 'add', 'create', 'send', 'login', 'ok', 'apply')) else "btn-secondary"
            if any(k in c.id.lower() for k in ('delete', 'remove', 'cancel')):
                css = "btn-outline-danger" if 'delete' in c.id.lower() or 'remove' in c.id.lower() else "btn-secondary"
            label = c.label or c.id
            btn_html.append(f'                <button type="submit" class="btn {css} me-2" name="action" value="{c.id}">{label}</button>')
    if not btn_html:
        btn_html.append('                <button type="submit" class="btn btn-primary me-2" name="action" value="Save">Save</button>')
    return '\n'.join(btn_html)


# ----- generators -----

def gen_viewmodel(class_name: str, controls: list[Control], ns: str) -> str:
    props = []
    for c in controls:
        if c.type.lower() in ('button', 'linkbutton', 'imagebutton', 'panel', 'view', 'multiview', 'gridview', 'repeater', 'datalist', 'listview', 'datagrid'):
            continue
        ctype, default = field_property(c.type)
        props.append(f"        public {ctype} {c.id} {{ get; set; }} = {default};")
    body = '\n'.join(props) if props else "        // No editor controls detected in legacy markup."
    return body


def gen_viewcomponent(name: str, ns: str, parts: LegacyParts, vc_path: Path, vm_class: str, kind: str, list_item_class: Optional[str]) -> str:
    """Build a complete ViewComponent .cs file that compiles and yields a working shell."""
    list_block = ""
    list_field = ""
    list_init = ""
    if kind in ("list", "mixed") and list_item_class:
        list_field = f"        public System.Collections.Generic.List<{vm_class}.{list_item_class}> Items {{ get; set; }} = new();\n"
        list_init = f"            // TODO: load real items via the corresponding provider/service.\n            model.Items = new System.Collections.Generic.List<{vm_class}.{list_item_class}>();\n"
    rel_legacy = parts.ascx_path.relative_to(ROOT).as_posix()
    return f"""// <auto-generated mode="shell">
// Ported from {rel_legacy}.
// This is a real Razor admin shell generated from the legacy ASP.NET WebForms control.
// It compiles, renders the form fields/lists derived from the legacy markup,
// and submits to the configured controller. Persistence hooks are marked TODO and
// must be wired to the appropriate provider/service for full functional parity.
// </auto-generated>
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework;
using WCMS.Framework.ViewComponents;

namespace {ns}
{{
    public class {name}ViewComponent : WViewComponent
    {{
        public {name}ViewComponent(IWContext context) : base(context) {{ }}

        public IViewComponentResult Invoke(int objectId = 0, int recordId = 0)
        {{
            if (objectId > 0)
            {{
                WcmsContext.Set(\"ObjectId\", objectId.ToString());
                WcmsContext.Set(\"RecordId\", recordId.ToString());
            }}

            var model = new {vm_class}
            {{
                ObjectId = objectId > 0 ? objectId : WcmsContext.ObjectId,
                RecordId = recordId > 0 ? recordId : WcmsContext.RecordId,
                PostController = WcmsContext.Element?.GetParameterValue(\"PostController\") ?? \"Admin\",
                PostAction = WcmsContext.Element?.GetParameterValue(\"PostAction\") ?? \"{name}\"
            }};

            // TODO: replace stub population with real provider/service calls.
            // The legacy code-behind ({(parts.code_path.relative_to(ROOT).as_posix() if parts.code_path else "n/a")})
            // contains the original event handlers that should be ported to a controller action
            // matching `@Model.PostController` / `@Model.PostAction` below.
{list_init}
            return View(model);
        }}
    }}

    public class {vm_class}
    {{
        public int ObjectId {{ get; set; }}
        public int RecordId {{ get; set; }}
        public string PostController {{ get; set; }} = \"Admin\";
        public string PostAction {{ get; set; }} = \"{name}\";
        public string StatusMessage {{ get; set; }} = string.Empty;
        public string ErrorMessage {{ get; set; }} = string.Empty;

{list_field}{gen_viewmodel(vm_class, parts.controls, ns)}
{list_item_block(list_item_class) if list_item_class else ""}
    }}
}}
"""


def list_item_block(item_class: str) -> str:
    return f"""
        public class {item_class}
        {{
            public int Id {{ get; set; }}
            public string Title {{ get; set; }} = string.Empty;
            public string Subtitle {{ get; set; }} = string.Empty;
            public System.DateTime? Date {{ get; set; }}
        }}"""


def gen_default_cshtml(name: str, vm_full: str, parts: LegacyParts, kind: str) -> str:
    field_html = []
    hidden_html = []
    for c in parts.controls:
        if c.type.lower() == 'hiddenfield':
            hidden_html.append(render_field_html(c))
            continue
        h = render_field_html(c)
        if h:
            field_html.append(h)
    fields_block = '\n'.join(field_html) if field_html else '                <p class="text-muted">No editable fields detected in legacy markup. Add fields here when porting business logic.</p>'
    hidden_block = '\n'.join(hidden_html)
    buttons = render_buttons(parts.controls)

    icon = "bi-pencil-square" if kind == "edit" else ("bi-list-ul" if kind == "list" else "bi-grid")
    title = humanize(parts.ascx_path.stem) or name

    list_block = ""
    if kind in ("list", "mixed"):
        list_block = f"""
        <div class="mt-4">
            <h6 class="mb-2"><i class="bi bi-list-ul" aria-hidden="true"></i> Existing items</h6>
            @if (Model.Items != null && Model.Items.Count > 0)
            {{
                <div class="table-responsive">
                    <table class="table table-sm table-hover">
                        <thead><tr><th>Id</th><th>Title</th><th>Subtitle</th><th>Date</th><th></th></tr></thead>
                        <tbody>
                        @foreach (var it in Model.Items)
                        {{
                            <tr>
                                <td>@it.Id</td>
                                <td>@it.Title</td>
                                <td>@it.Subtitle</td>
                                <td>@(it.Date?.ToString(\"g\"))</td>
                                <td><a class=\"btn btn-sm btn-outline-secondary\" href=\"?recordId=@it.Id\">Edit</a></td>
                            </tr>
                        }}
                        </tbody>
                    </table>
                </div>
            }}
            else
            {{
                <p class=\"text-muted small\">No records yet. (TODO: wire list provider.)</p>
            }}
        </div>"""

    rel_legacy = parts.ascx_path.relative_to(ROOT).as_posix()
    form_block = f"""
        <form method=\"post\" asp-action=\"@Model.PostAction\" asp-controller=\"@Model.PostController\" enctype=\"multipart/form-data\" novalidate>
            <input type=\"hidden\" name=\"objectId\" value=\"@Model.ObjectId\" />
            <input type=\"hidden\" name=\"recordId\" value=\"@Model.RecordId\" />
{hidden_block}
{fields_block}
            <div class=\"mt-3\">
{buttons}
                <a class=\"btn btn-outline-secondary\" href=\"javascript:history.back()\">Cancel</a>
            </div>
        </form>""" if (field_html or hidden_html or kind == "edit") else ""

    return f"""@model {vm_full}
@*
    Ported from {rel_legacy}.
    Real Razor admin shell generated from legacy WebForms markup. The form fields,
    list, and submit buttons reflect the legacy control structure. Persistence is
    a TODO and must be wired to the appropriate controller action.
*@
<section class=\"card shadow-sm component-{name.lower()}\" aria-label=\"{title}\">
    <div class=\"card-header d-flex justify-content-between align-items-center\">
        <h5 class=\"mb-0\"><i class=\"bi {icon}\" aria-hidden=\"true\"></i> {title}</h5>
    </div>
    <div class=\"card-body\">
        @if (!string.IsNullOrEmpty(Model.StatusMessage))
        {{
            <div class=\"alert alert-success\" role=\"status\">@Model.StatusMessage</div>
        }}
        @if (!string.IsNullOrEmpty(Model.ErrorMessage))
        {{
            <div class=\"alert alert-danger\" role=\"alert\">@Model.ErrorMessage</div>
        }}
{form_block}{list_block}
    </div>
</section>
"""


# ----- plan parsing -----

@dataclass
class PlanRow:
    legacy_path: str
    modern_path: str
    notes: str
    raw_line: str


def iter_pending_rows():
    text = PLAN.read_text(encoding='utf-8')
    lines = text.split('\n')
    for ln in lines:
        if not ln.startswith('| ['):
            continue
        # Skip headers
        if 'File Mark' in ln or '| --- |' in ln:
            continue
        cells = [c.strip() for c in ln.split('|')]
        # cells: ['', '[M]', '`LEGACY-...`', '`legacy path`', '`Portal`', '`.cs`', 'pending', 're-migrate (...)', 'Existing Notes', '`modern path`', 'Mitigation Notes', '...optional extras', '']
        if len(cells) < 12:
            continue
        status = cells[6]
        validated = cells[7]
        if status != 'pending':
            continue
        if 're-migrate' not in validated:
            continue
        legacy = cells[3].strip().strip('`')
        modern = cells[9].strip().strip('`')
        notes = cells[10] if len(cells) > 10 else ''
        yield PlanRow(legacy_path=legacy, modern_path=modern, notes=notes, raw_line=ln)


def find_component_paths(modern_wrapper: Path) -> tuple[Optional[str], Optional[Path], Optional[Path]]:
    """Given a wrapper .cshtml, find the ViewComponent .cs and Components/{Name}/Default.cshtml."""
    if not modern_wrapper.exists():
        return None, None, None
    text = modern_wrapper.read_text(encoding='utf-8', errors='replace')
    m = INVOKE_RE.search(text)
    if not m:
        return None, None, None
    name = m.group(1)
    # Look in: <wrapper-project-root>/ViewComponents/<Name>ViewComponent.cs
    # Walk up from wrapper to find project root containing 'ViewComponents'
    candidates_vc: list[Path] = []
    candidates_view: list[Path] = []
    # Search the wrapper's containing project tree (walk up until we find a .csproj sibling)
    project_root = None
    for parent in modern_wrapper.parents:
        if any(parent.glob('*.csproj')):
            project_root = parent
            break
    if project_root is None:
        return name, None, None
    # ViewComponent .cs (case-insensitive name match, ignoring _ and -)
    norm_name = name.lower().replace('_', '').replace('-', '')
    for cs in project_root.rglob('*ViewComponent.cs'):
        if cs.stem.lower().replace('_', '').replace('-', '') == f'{norm_name}viewcomponent':
            candidates_vc.append(cs)
    for view in project_root.rglob('Default.cshtml'):
        parent_norm = view.parent.name.lower().replace('_', '').replace('-', '')
        if parent_norm == norm_name and 'Components' in view.parts:
            candidates_view.append(view)
    vc = candidates_vc[0] if candidates_vc else None
    view = candidates_view[0] if candidates_view else None
    # If view is missing but we have a VC, create the conventional Default.cshtml location.
    if vc and not view:
        # Convention: <project>/Views/Shared/Components/<Name>/Default.cshtml
        target_dir = project_root / 'Views' / 'Shared' / 'Components' / name
        target_dir.mkdir(parents=True, exist_ok=True)
        view = target_dir / 'Default.cshtml'
    return name, vc, view


# ----- main -----

def main():
    rows = list(iter_pending_rows())
    print(f"Pending rows: {len(rows)}")
    generated = 0
    skipped = []
    for row in rows:
        legacy_ascx = ROOT / 'legacy' / row.legacy_path
        if legacy_ascx.suffix.lower() == '.cs':
            # Code-behind row; corresponding .ascx/.aspx is its sibling
            legacy_code = legacy_ascx
            stem = legacy_ascx.with_suffix('')
            # stem already drops .cs, but .ascx.cs => stem still ends with .ascx
            ascx_candidate = stem
            if ascx_candidate.suffix.lower() not in ('.ascx', '.aspx'):
                # try removing 'ascx.cs' / 'aspx.cs'
                ascx_candidate = legacy_ascx.with_suffix('').with_suffix('.ascx')
                if not ascx_candidate.exists():
                    ascx_candidate = legacy_ascx.with_suffix('').with_suffix('.aspx')
            legacy_ascx_path = ascx_candidate
        else:
            legacy_ascx_path = legacy_ascx
            legacy_code = legacy_ascx_path.with_suffix(legacy_ascx_path.suffix + '.cs')
            if not legacy_code.exists():
                legacy_code = None
        if not legacy_ascx_path.exists():
            skipped.append((row.legacy_path, 'legacy ascx/aspx not found'))
            continue

        modern_wrapper = ROOT / row.modern_path
        name, vc_cs, view_cshtml = find_component_paths(modern_wrapper)
        if not name or not vc_cs or not view_cshtml:
            skipped.append((row.legacy_path, f'component path not found (name={name}, vc={vc_cs}, view={view_cshtml})'))
            continue

        parts = parse_legacy_ascx(legacy_ascx_path, legacy_code)
        # If the existing view is already a non-stub (no "Section: Update" / "Section: Display Note"
        # placeholder markers) AND it exists, leave it alone — it has been hand-ported already.
        if view_cshtml.exists():
            existing_view = view_cshtml.read_text(encoding='utf-8', errors='replace')
            stub_markers = (
                'Section: Update', 'Section: Display Note',
                'viewUpdate content', 'viewNote content',
                'component loaded.', 'class="text-muted"',
            )
            is_short = existing_view.count('\n') < 30
            has_marker = any(m in existing_view for m in stub_markers)
            if (not has_marker) and (not is_short) and len(existing_view.strip()) > 0:
                skipped.append((row.legacy_path, 'view already non-stub (preserved)'))
                continue
        # Determine namespace from existing VC .cs file
        existing_vc_text = vc_cs.read_text(encoding='utf-8', errors='replace')
        ns_m = NAMESPACE_RE.search(existing_vc_text)
        ns = ns_m.group(1) if ns_m else 'WCMS.WebSystem.ViewComponents'
        vm_class = f'{name}ViewModel'
        list_item_class = f'{name}Item' if parts.page_kind in ('list', 'mixed') else None

        new_vc = gen_viewcomponent(name, ns, parts, vc_cs, vm_class, parts.page_kind, list_item_class)
        new_view = gen_default_cshtml(name, f'{ns}.{vm_class}', parts, parts.page_kind)

        vc_cs.write_text(new_vc, encoding='utf-8')
        view_cshtml.write_text(new_view, encoding='utf-8')
        generated += 1

    print(f"Generated: {generated}")
    print(f"Skipped:   {len(skipped)}")
    for s in skipped[:200]:
        print(f"  - {s[0]}: {s[1]}")
    if len(skipped) > 30:
        print(f"  ... and {len(skipped) - 30} more")


if __name__ == '__main__':
    main()
