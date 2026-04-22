#!/usr/bin/env python3
"""Add 'Validated' column to legacy-gap-plan.md and validate every completed row.

For each [M] row whose Current Status is `completed`:
- Read the legacy file (under legacy/<path>) and, for code-behinds, the paired
  legacy .ascx UI.
- Read the modern file from the Modern File column. If it's an auto-migrated
  wrapper that delegates to a ViewComponent, also read the ViewComponent .cs
  and Default.cshtml, and validate against THAT body.
- Compare structural signals (legacy controls + event handlers vs modern form
  fields + actions). Detect stub markers in the modern body.
- Emit one of: `validated`, `re-migrate (code)`, `re-migrate (UI)`,
  `re-migrate (code+UI)`. Place the value in the new `Validated` column.
- When re-migration is needed, also update Current Status to `pending` and
  append a short validation summary to Mitigation/Implementation Notes.

For non-completed rows, Validated = `—`.

Idempotent: detects already-restructured 10-column rows and recomputes.
"""
from __future__ import annotations

import os
import re
import sys

REPO_ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
PLAN = os.path.join(REPO_ROOT, "docs", "plans", "legacy-migration-v2", "legacy-gap-plan.md")

# ---------------------------------------------------------------------------
# Header transforms (9-col -> 10-col)
# ---------------------------------------------------------------------------
OLD_HDR = "| File Mark | ID | Legacy File (relative to Legacy/) | Module | File Type | Current Status | Existing Notes | Modern File | Mitigation/Implementation Notes |"
OLD_SEP = "| --- | --- | --- | --- | --- | --- | --- | --- | --- |"
NEW_HDR = "| File Mark | ID | Legacy File (relative to Legacy/) | Module | File Type | Current Status | Validated | Existing Notes | Modern File | Mitigation/Implementation Notes |"
NEW_SEP = "| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |"

# ---------------------------------------------------------------------------
# Regexes
# ---------------------------------------------------------------------------
ASP_CONTROL_RE = re.compile(
    r'<asp:(\w+)\b[^>]*?\bID\s*=\s*"([^"]+)"', re.IGNORECASE | re.DOTALL,
)
EVENT_HANDLER_RE = re.compile(
    r'\b(?:protected|public|private|internal)\s+(?:async\s+)?(?:void|Task)\s+'
    r'(\w+_(?:Click|ItemCommand|SelectedIndexChanged|TextChanged|CheckedChanged|'
    r'RowCommand|RowDataBound|ItemDataBound|Command|Init|Load|PreRender|DataBound))\s*\('
)
WRAPPER_INVOKE_RE = re.compile(r'Component\.InvokeAsync\(\s*"([^"]+)"')
MODERN_FIELD_RE = re.compile(
    r'\b(?:name|asp-for|id)\s*=\s*"([^"]+)"', re.IGNORECASE,
)
MODERN_ACTION_RE = re.compile(
    r'\b(?:asp-action|asp-controller|asp-route|formaction)\s*=\s*"([^"]+)"',
    re.IGNORECASE,
)
RAZOR_HELPER_RE = re.compile(
    r'@Html\.\w+|@Url\.Action|@using\s*\(Html\.BeginForm', re.IGNORECASE,
)
HARD_STUB_MARKERS = [
    "Section: Update",
    "Section: Display",
    "Section content",
    "Section: Insert",
    "Section: Edit",
    "Section: List",
    "Section: View",
    'class="text-muted"',
    'p.text-muted',
    "viewUpdate content",
    "viewDisplay content",
    "viewInsert content",
    "viewEdit content",
    "TODO",
    "NotImplementedException",
    "Auto-migrated compatibility wrapper",
    "Migration mode: Compatibility Wrapper",
]

REASON_HEADING = re.compile(r"^### Reason: `([^`]+)` \((\d+) files\)\s*$")


# ---------------------------------------------------------------------------
# IO
# ---------------------------------------------------------------------------
def read_file_text(rel_path: str) -> str | None:
    p = os.path.join(REPO_ROOT, rel_path)
    try:
        with open(p, "r", encoding="utf-8", errors="replace") as f:
            return f.read()
    except (FileNotFoundError, IsADirectoryError):
        return None


# ---------------------------------------------------------------------------
# ViewComponent index (build once)
# ---------------------------------------------------------------------------
_VC_CS: dict[str, list[str]] = {}
_VC_VIEW: dict[str, list[str]] = {}
_INDEX_BUILT = False
SKIP_DIRS = {"bin", "obj", "node_modules", ".git", "legacy", ".vs", "packages"}


def build_index() -> None:
    global _INDEX_BUILT
    if _INDEX_BUILT:
        return
    for root, dirs, files in os.walk(REPO_ROOT):
        dirs[:] = [d for d in dirs if d not in SKIP_DIRS]
        for fn in files:
            full = os.path.join(root, fn)
            rel = os.path.relpath(full, REPO_ROOT)
            if fn.lower().endswith("viewcomponent.cs"):
                key = fn[: -len("ViewComponent.cs")].lower()
                _VC_CS.setdefault(key, []).append(rel)
            if fn == "Default.cshtml":
                parent = os.path.basename(root)
                gp = os.path.basename(os.path.dirname(root))
                if gp == "Components":
                    _VC_VIEW.setdefault(parent.lower(), []).append(rel)
    _INDEX_BUILT = True


def find_view_component(name: str) -> tuple[str | None, str | None]:
    build_index()
    cs = (_VC_CS.get(name.lower()) or [None])[0]
    view = (_VC_VIEW.get(name.lower()) or [None])[0]
    return cs, view


# ---------------------------------------------------------------------------
# Heuristics
# ---------------------------------------------------------------------------
def stub_score(text: str) -> int:
    if not text:
        return 0
    return sum(1 for m in HARD_STUB_MARKERS if m in text)


def extract_modern_signals(text: str) -> tuple[set[str], set[str], int]:
    """Returns (form_fields, actions, razor_helper_hits)."""
    fields = set(MODERN_FIELD_RE.findall(text))
    # Drop common boilerplate IDs
    fields = {f for f in fields if not f.lower().startswith(("alert", "view-"))}
    actions = set(MODERN_ACTION_RE.findall(text))
    helpers = len(RAZOR_HELPER_RE.findall(text))
    return fields, actions, helpers


def extract_legacy_signals(ui_text: str | None, cb_text: str | None) -> tuple[set[str], set[str]]:
    controls: set[str] = set()
    if ui_text:
        for _, cid in ASP_CONTROL_RE.findall(ui_text):
            controls.add(cid.lower())
    handlers: set[str] = set()
    if cb_text:
        for h in EVENT_HANDLER_RE.findall(cb_text):
            handlers.add(h)
    return controls, handlers


# ---------------------------------------------------------------------------
# Per-row validators
# ---------------------------------------------------------------------------
def validate_codebehind(legacy_path: str, modern_path: str) -> tuple[str, str]:
    """legacy_path is the legacy code-behind (.ascx.cs / .aspx.cs).
    modern_path is the modern .cshtml.
    Returns (validated_value, detail)."""
    legacy_ui_path = re.sub(r"\.cs$", "", legacy_path)
    legacy_cb = read_file_text(f"legacy/{legacy_path}")
    legacy_ui = read_file_text(f"legacy/{legacy_ui_path}")
    modern = read_file_text(modern_path)

    if modern is None:
        return ("re-migrate (code+UI)", "modern file not found on disk")

    legacy_controls, legacy_handlers = extract_legacy_signals(legacy_ui, legacy_cb)
    legacy_size = (len(legacy_cb or "") + len(legacy_ui or ""))

    # Resolve wrapper -> ViewComponent
    body_text = modern
    vc_text = ""
    view_text = ""
    used_wrapper = False
    wm = WRAPPER_INVOKE_RE.search(modern) if len(modern) < 1500 else None
    if wm:
        used_wrapper = True
        vc_path, view_path = find_view_component(wm.group(1))
        if vc_path:
            vc_text = read_file_text(vc_path) or ""
        if view_path:
            view_text = read_file_text(view_path) or ""
        body_text = modern + "\n" + vc_text + "\n" + view_text

    fields, actions, helpers = extract_modern_signals(body_text)
    modern_complexity = len(fields) + len(actions) + (helpers // 2)
    stub_hits = stub_score(view_text or modern) + stub_score(vc_text)

    legacy_complexity = len(legacy_controls) + len(legacy_handlers)

    # Trivial legacy → accept any modern presence
    if legacy_complexity <= 1 and len(legacy_handlers) == 0 and legacy_size < 1500:
        return ("validated", f"trivial legacy ({legacy_size}B); modern present")

    # Strong stub signal in modern view body → re-migrate code+UI
    if stub_hits >= 2 and legacy_complexity >= 3:
        return (
            "re-migrate (code+UI)",
            f"modern is auto-migrated stub ({stub_hits} stub markers); legacy has {len(legacy_controls)} controls + {len(legacy_handlers)} handlers",
        )

    # Modern view has no form fields and no actions, but legacy did
    if modern_complexity == 0 and legacy_complexity >= 3:
        target = "re-migrate (code+UI)" if used_wrapper else "re-migrate (UI)"
        return (
            target,
            f"modern body has 0 form fields/actions; legacy has {len(legacy_controls)} controls + {len(legacy_handlers)} handlers",
        )

    # Legacy had handlers but modern has no actions → server-side logic missing
    if len(legacy_handlers) >= 2 and len(actions) == 0 and helpers == 0:
        return (
            "re-migrate (code)",
            f"legacy has {len(legacy_handlers)} event handlers but modern body has no asp-action / @Html.* / Url.Action",
        )

    # Heavy legacy UI but sparse modern fields
    if len(legacy_controls) >= 6 and len(fields) < max(2, len(legacy_controls) // 4):
        return (
            "re-migrate (UI)",
            f"legacy has {len(legacy_controls)} server controls; modern body exposes only {len(fields)} fields",
        )

    return (
        "validated",
        f"modern body has {len(fields)} fields, {len(actions)} actions, {helpers} razor helpers vs legacy {len(legacy_controls)}c/{len(legacy_handlers)}h",
    )


def validate_aspx_view(legacy_path: str, modern_path: str) -> tuple[str, str]:
    """legacy_path is .ascx or .aspx (no code-behind). Compare directly."""
    legacy_ui = read_file_text(f"legacy/{legacy_path}")
    modern = read_file_text(modern_path)
    if modern is None:
        return ("re-migrate (UI)", "modern file not found on disk")
    legacy_controls, _ = extract_legacy_signals(legacy_ui, None)
    body_text = modern
    wm = WRAPPER_INVOKE_RE.search(modern) if len(modern) < 1500 else None
    used_wrapper = False
    view_text = ""
    if wm:
        used_wrapper = True
        _, view_path = find_view_component(wm.group(1))
        if view_path:
            view_text = read_file_text(view_path) or ""
        body_text = modern + "\n" + view_text
    fields, actions, helpers = extract_modern_signals(body_text)
    stub_hits = stub_score(view_text or modern)
    if len(legacy_controls) <= 1:
        return ("validated", f"trivial legacy UI; modern present")
    if stub_hits >= 2 and len(legacy_controls) >= 3:
        return ("re-migrate (UI)", f"modern body is stub ({stub_hits} markers); legacy has {len(legacy_controls)} controls")
    if (len(fields) + len(actions)) == 0 and len(legacy_controls) >= 3:
        target = "re-migrate (code+UI)" if used_wrapper else "re-migrate (UI)"
        return (target, f"modern body has 0 fields/actions vs legacy {len(legacy_controls)} controls")
    return ("validated", f"modern body has {len(fields)} fields, {len(actions)} actions vs legacy {len(legacy_controls)} controls")


def validate_generic(legacy_path: str, modern_path: str, file_type: str) -> tuple[str, str]:
    modern = read_file_text(modern_path)
    if modern is None:
        return ("re-migrate (code)", "modern file not found on disk")
    if len(modern.strip()) < 30:
        return ("re-migrate (code)", f"modern file essentially empty ({len(modern)}B)")
    return ("validated", f"modern file present ({len(modern)}B)")


def validate_row(legacy_path: str, modern_path: str, file_type: str) -> tuple[str, str]:
    if not modern_path or modern_path == "—":
        return ("—", "no modern file")
    if legacy_path.endswith(".ascx.cs") or legacy_path.endswith(".aspx.cs"):
        return validate_codebehind(legacy_path, modern_path)
    if legacy_path.endswith(".ascx") or legacy_path.endswith(".aspx"):
        return validate_aspx_view(legacy_path, modern_path)
    return validate_generic(legacy_path, modern_path, file_type)


# ---------------------------------------------------------------------------
# Markdown rewrite
# ---------------------------------------------------------------------------
def split_row(line: str) -> list[str]:
    s = line.rstrip("\n")
    if s.startswith("|"):
        s = s[1:]
    if s.endswith("|"):
        s = s[:-1]
    return [c.strip() for c in s.split("|")]


def join_row(cells: list[str]) -> str:
    return "| " + " | ".join(cells) + " |\n"


def transform_file_row(cells: list[str]) -> tuple[list[str], str]:
    """Return (new_cells, validated_value).

    Old (9): [Mark, ID, Legacy, Module, FileType, Status, Existing Notes, Modern, Notes]
    New (10): [Mark, ID, Legacy, Module, FileType, Status, Validated, Existing Notes, Modern, Notes]
    """
    if len(cells) == 10:
        cells = cells[:6] + cells[7:]  # drop existing Validated; recompute
    if len(cells) != 9:
        return cells, ""
    file_mark, lid, legacy, module, ftype, status, existing, modern, notes = cells
    legacy_path = legacy.strip("` ").strip()
    modern_path_raw = modern.strip()
    modern_path = modern_path_raw.strip("` ").strip()
    if modern_path == "—":
        modern_path = ""

    if file_mark == "[M]" and status == "completed":
        validated, detail = validate_row(legacy_path, modern_path, ftype)
        if validated.startswith("re-migrate"):
            status = "pending"
            notes = notes + f" | Validation ({validated}): {detail}."
    else:
        validated = "—"

    new_cells = [file_mark, lid, legacy, module, ftype, status, validated, existing, modern, notes]
    return new_cells, validated


def main() -> int:
    build_index()
    print(f"VC index: {len(_VC_CS)} ViewComponents, {len(_VC_VIEW)} Default.cshtml views")
    with open(PLAN, "r", encoding="utf-8") as f:
        lines = f.readlines()
    out: list[str] = []
    in_file_table = False
    counts: dict[str, int] = {}
    for line in lines:
        stripped = line.rstrip("\n")
        if REASON_HEADING.match(line):
            in_file_table = False
            out.append(line)
            continue
        if stripped == OLD_HDR or stripped == NEW_HDR:
            in_file_table = True
            out.append(NEW_HDR + "\n")
            continue
        if in_file_table and (stripped == OLD_SEP or stripped == NEW_SEP):
            out.append(NEW_SEP + "\n")
            continue
        if in_file_table and line.startswith("|"):
            cells = split_row(line)
            new_cells, validated = transform_file_row(cells)
            counts[validated or "(skipped)"] = counts.get(validated or "(skipped)", 0) + 1
            out.append(join_row(new_cells))
            continue
        if in_file_table and not line.startswith("|"):
            in_file_table = False
        out.append(line)
    with open(PLAN, "w", encoding="utf-8") as f:
        f.writelines(out)
    print("Validation outcome counts:")
    for k in sorted(counts):
        print(f"  {k:30s} {counts[k]}")
    return 0


if __name__ == "__main__":
    sys.exit(main())
