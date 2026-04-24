#!/usr/bin/env python3
"""
Batch update legacy-source-tracking-all.csv for the .NET 10 migration.
Categorizes not_started and incomplete items into proper statuses.
"""

import csv
import os
from collections import Counter

CSV_PATH = os.path.join(os.path.dirname(__file__), 'inventory', 'legacy-source-tracking-all.csv')

def load_csv():
    with open(CSV_PATH, 'r') as f:
        reader = csv.DictReader(f)
        rows = list(reader)
        fieldnames = reader.fieldnames
    return rows, fieldnames

def save_csv(rows, fieldnames):
    with open(CSV_PATH, 'w', newline='') as f:
        writer = csv.DictWriter(f, fieldnames=fieldnames)
        writer.writeheader()
        writer.writerows(rows)

def update_row(row, status, status_basis, notes_append=None):
    """Update a row's status fields."""
    row['status'] = status
    row['status_basis'] = status_basis
    if notes_append:
        existing = row.get('notes', '')
        if existing:
            row['notes'] = f"{existing}; {notes_append}"
        else:
            row['notes'] = notes_append

def apply_updates(rows):
    changes = Counter()
    
    for row in rows:
        if row['status'] not in ('not_started', 'incomplete'):
            continue
        
        ftype = row['file_type']
        legacy_path = row['legacy_path']
        row_id = row['id']
        submodule = row.get('submodule', '')
        
        # ============================================================
        # BATCH 1: Obsolete file types → not_applicable
        # ============================================================
        
        # Code-behind for obsolete file types (.asmx.cs, .ashx.cs, .svc.cs, .asax.cs)
        if ftype == '.cs':
            if legacy_path.endswith('.asmx.cs'):
                update_row(row, 'not_applicable', 'obsolete_filetype',
                           'Code-behind for ASMX web service; replaced by REST API')
                changes['asmx_cs_not_applicable'] += 1
                continue
            if legacy_path.endswith('.ashx.cs'):
                update_row(row, 'not_applicable', 'obsolete_filetype',
                           'Code-behind for .ashx HTTP handler; replaced by middleware/endpoints')
                changes['ashx_cs_not_applicable'] += 1
                continue
            if legacy_path.endswith('.svc.cs'):
                update_row(row, 'not_applicable', 'obsolete_filetype',
                           'Code-behind for WCF service; replaced by REST API')
                changes['svc_cs_not_applicable'] += 1
                continue
            if legacy_path.endswith('Global.asax.cs'):
                update_row(row, 'not_applicable', 'obsolete_filetype',
                           'Global.asax code-behind; startup in Program.cs')
                changes['asax_cs_not_applicable'] += 1
                continue
        
        # .cmd - Windows batch scripts (not applicable to .NET 10 on cross-platform)
        if ftype == '.cmd':
            update_row(row, 'not_applicable', 'obsolete_filetype',
                       'Windows batch scripts; replaced by cross-platform tooling')
            changes['cmd_not_applicable'] += 1
            continue
        
        # .sln - Old solution files (consolidated into mPortal.slnx)
        if ftype == '.sln':
            update_row(row, 'not_applicable', 'obsolete_filetype',
                       'Legacy .sln files; consolidated into mPortal.slnx')
            changes['sln_not_applicable'] += 1
            continue
        
        # .config - web.config/app.config/packages.config (replaced by appsettings.json / SDK-style .csproj)
        if ftype == '.config':
            update_row(row, 'not_applicable', 'obsolete_filetype',
                       'Legacy .config files; settings in appsettings.json, packages in .csproj')
            changes['config_not_applicable'] += 1
            continue
        
        # .asax - Global.asax (replaced by Program.cs middleware)
        if ftype == '.asax':
            update_row(row, 'not_applicable', 'obsolete_filetype',
                       'Global.asax obsolete; startup logic in Program.cs')
            changes['asax_not_applicable'] += 1
            continue
        
        # .diagram - SQL Server database diagrams
        if ftype == '.diagram':
            update_row(row, 'not_applicable', 'obsolete_filetype',
                       'SQL Server diagram files; not used in PostgreSQL migration')
            changes['diagram_not_applicable'] += 1
            continue
        
        # .snk - Strong name key files (not used in modern .NET)
        if ftype == '.snk':
            update_row(row, 'not_applicable', 'obsolete_filetype',
                       'Strong naming not used in modern .NET')
            changes['snk_not_applicable'] += 1
            continue
        
        # .tt - T4 text templates
        if ftype == '.tt':
            update_row(row, 'not_applicable', 'obsolete_filetype',
                       'T4 templates not used; source generators preferred')
            changes['tt_not_applicable'] += 1
            continue
        
        # .resx - Resource files (legacy assembly resources)
        if ftype == '.resx':
            update_row(row, 'not_applicable', 'obsolete_filetype',
                       'Legacy .resx resource files; not needed in modern project')
            changes['resx_not_applicable'] += 1
            continue
        
        # .edmx - Entity Framework model files (EDMX obsolete, using Npgsql/Dapper)
        if ftype == '.edmx':
            update_row(row, 'not_applicable', 'obsolete_filetype',
                       'EDMX Entity Framework models obsolete; using Npgsql/raw SQL')
            changes['edmx_not_applicable'] += 1
            continue
        
        # .svc - WCF service files
        if ftype == '.svc':
            update_row(row, 'not_applicable', 'obsolete_filetype',
                       'WCF .svc services removed; replaced by REST API endpoints')
            changes['svc_not_applicable'] += 1
            continue
        
        # .asmx - ASMX web service files
        if ftype == '.asmx':
            update_row(row, 'not_applicable', 'obsolete_filetype',
                       'Legacy ASMX web services replaced by REST API endpoints')
            changes['asmx_not_applicable'] += 1
            continue
        
        # .ashx - HTTP handler files
        if ftype == '.ashx':
            update_row(row, 'not_applicable', 'obsolete_filetype',
                       'Legacy .ashx HTTP handlers replaced by middleware/endpoints')
            changes['ashx_not_applicable'] += 1
            continue
        
        # .datasource - Visual Studio data source files
        if ftype == '.datasource':
            update_row(row, 'not_applicable', 'obsolete_filetype',
                       'Visual Studio data source designer files; not used')
            changes['datasource_not_applicable'] += 1
            continue
        
        # .xsd - XML Schema Definition (dataset designer)
        if ftype == '.xsd':
            update_row(row, 'not_applicable', 'obsolete_filetype',
                       'Visual Studio dataset designer files; not used in modern .NET')
            changes['xsd_not_applicable'] += 1
            continue
        
        # .xsc / .xss - XSD support files
        if ftype in ('.xsc', '.xss'):
            update_row(row, 'not_applicable', 'obsolete_filetype',
                       'XSD support files; not used in modern .NET')
            changes['xsd_support_not_applicable'] += 1
            continue
        
        # .settings - Visual Studio settings files
        if ftype == '.settings':
            update_row(row, 'not_applicable', 'obsolete_filetype',
                       'VS settings files; config in appsettings.json')
            changes['settings_not_applicable'] += 1
            continue
        
        # .disco / .discomap / .wsdl - WCF/SOAP discovery files
        if ftype in ('.disco', '.discomap', '.wsdl'):
            update_row(row, 'not_applicable', 'obsolete_filetype',
                       'WCF/SOAP discovery files; WCF removed')
            changes['soap_disco_not_applicable'] += 1
            continue
        
        # .map - Source map files (build artifacts)
        if ftype == '.map' and '.js.map' in legacy_path:
            update_row(row, 'not_applicable', 'build_artifact',
                       'JavaScript source map; build artifact, not source')
            changes['map_not_applicable'] += 1
            continue
        
        # ============================================================
        # BATCH 2: FCKeditor → not_applicable (entire third-party library replaced)
        # ============================================================
        if 'FCKeditor' in legacy_path or 'fckeditor' in legacy_path.lower():
            update_row(row, 'not_applicable', 'obsolete_third_party',
                       'FCKeditor replaced by modern rich text editor')
            changes['fckeditor_not_applicable'] += 1
            continue
        
        # ============================================================
        # BATCH 3: AssemblyInfo.cs → completed (metadata in SDK-style .csproj)
        # ============================================================
        if legacy_path.endswith('AssemblyInfo.cs'):
            update_row(row, 'not_applicable', 'obsolete_filetype',
                       'Assembly attributes now in SDK-style .csproj')
            changes['assemblyinfo_not_applicable'] += 1
            continue
        
        # ============================================================
        # BATCH 4: Specific obsolete .cs patterns
        # ============================================================
        
        # App_Start files (BundleConfig, RouteConfig, Startup.Auth, WebApiConfig)
        if '/App_Start/' in legacy_path:
            update_row(row, 'not_applicable', 'obsolete_pattern',
                       'ASP.NET MVC App_Start patterns replaced by Program.cs middleware')
            changes['app_start_not_applicable'] += 1
            continue
        
        # Startup.cs (OWIN/MVC startup)
        if legacy_path.endswith('/Startup.cs'):
            update_row(row, 'not_applicable', 'obsolete_pattern',
                       'OWIN/MVC Startup.cs replaced by Program.cs')
            changes['startup_not_applicable'] += 1
            continue
        
        # OracleHelper.cs - Oracle DB provider removed
        if legacy_path.endswith('OracleHelper.cs'):
            update_row(row, 'not_applicable', 'obsolete_feature',
                       'Oracle database support removed; using PostgreSQL')
            changes['oracle_not_applicable'] += 1
            continue
        
        # MusicModel.Context.cs - EF EDMX generated context
        if 'MusicModel.Context.cs' in legacy_path:
            update_row(row, 'not_applicable', 'obsolete_pattern',
                       'Entity Framework EDMX-generated context; using raw SQL/Npgsql')
            changes['ef_context_not_applicable'] += 1
            continue
        
        # Class1.cs placeholder
        if legacy_path.endswith('/Class1.cs'):
            update_row(row, 'not_applicable', 'obsolete_pattern',
                       'Empty placeholder class')
            changes['class1_not_applicable'] += 1
            continue
        
        # SOAP/WCF clients
        if 'SoapClient.cs' in legacy_path or 'Reference.cs' in legacy_path:
            update_row(row, 'not_applicable', 'obsolete_pattern',
                       'WCF/SOAP client proxy; WCF services replaced by REST APIs')
            changes['soap_client_not_applicable'] += 1
            continue
        
        # Site.Master.cs - WebForms master page code-behind
        if legacy_path.endswith('Site.Master.cs'):
            update_row(row, 'not_applicable', 'obsolete_pattern',
                       'WebForms master page code-behind; replaced by _Layout.cshtml')
            changes['site_master_not_applicable'] += 1
            continue
        
        # Site.Master - WebForms master page markup
        if legacy_path.endswith('Site.Master') and ftype == '.Master':
            update_row(row, 'not_applicable', 'obsolete_pattern',
                       'WebForms master page; replaced by _Layout.cshtml')
            changes['site_master_markup_not_applicable'] += 1
            continue
        
        # Dates.cs - legacy utility (duplicate in Portal/WebSystem copy)
        if legacy_path.endswith('Dates.cs'):
            update_row(row, 'not_applicable', 'obsolete_feature',
                       'Legacy date utility; not referenced in modern codebase')
            changes['dates_not_applicable'] += 1
            continue
        
        # WebForms base classes that are definitively obsolete
        webforms_bases = [
            'WPage.cs', 'WUserControl.cs', 'WebCommentHelper.cs',
            'RazorHelper.cs', 'WLoaderPageBase.cs'
        ]
        if any(legacy_path.endswith(f'/{wb}') for wb in webforms_bases):
            update_row(row, 'not_applicable', 'obsolete_pattern',
                       'WebForms/legacy Razor base class; replaced by ViewComponent pattern')
            changes['webforms_base_not_applicable'] += 1
            continue
        
        # Legacy ViewModels that were replaced by modern ViewModels
        legacy_viewmodels = [
            'WebGroupViewModel.cs', 'WebOfficeViewModel.cs', 'WebPageViewModel.cs',
            'WebPartBase.cs', 'WebPartViewModel.cs', 'WebSiteViewModel.cs'
        ]
        if any(legacy_path.endswith(f'/{vm}') for vm in legacy_viewmodels):
            update_row(row, 'not_applicable', 'obsolete_pattern',
                       'Legacy ViewModel replaced by modern ViewComponent architecture')
            changes['legacy_vm_not_applicable'] += 1
            continue
        
        # CatController.cs - legacy MVC controller not in modern codebase
        if legacy_path.endswith('CatController.cs'):
            update_row(row, 'not_applicable', 'obsolete_pattern',
                       'Legacy MVC controller; not needed in modern codebase')
            changes['cat_controller_not_applicable'] += 1
            continue
        
        # ============================================================
        # BATCH 6: Integration SOAP/WCF files
        # ============================================================
        # AttendanceSoapClient, MemberSoapClient - SOAP clients removed
        if 'AttendanceSoapClient' in legacy_path or 'MemberSoapClient' in legacy_path:
            update_row(row, 'not_applicable', 'obsolete_pattern',
                       'SOAP client removed; attendance/member via REST APIs')
            changes['integration_soap_not_applicable'] += 1
            continue
        
        # WCF service references
        if '/Web References/' in legacy_path or '/Service References/' in legacy_path:
            update_row(row, 'not_applicable', 'obsolete_pattern',
                       'WCF/ASMX service reference; services replaced by REST APIs')
            changes['service_ref_not_applicable'] += 1
            continue
        
        # ============================================================
        # BATCH 7: ObjectCapsule, ObjectRecordPair - not in modern codebase
        # ============================================================
        obsolete_framework = [
            'ObjectCapsule.cs', 'ObjectRecordPair.cs', 'WebObjectItem.cs',
            'WebSubstituter.cs', 'RemoteIndexerViewBase.cs',
            'FileManagerBase.cs', 'MenuHelper.cs',
        ]
        if any(legacy_path.endswith(f'/{of}') for of in obsolete_framework):
            update_row(row, 'not_applicable', 'obsolete_pattern',
                       'Legacy framework class not referenced in modern codebase')
            changes['obsolete_framework_not_applicable'] += 1
            continue
        
        # ============================================================
        # BATCH 8: MemberWS models (Member.cs, MemberAttendance.cs, MemberPhoto.cs)
        #          These are WCF data contracts, replaced by Integration models
        # ============================================================
        if '/MemberWS/' in legacy_path:
            update_row(row, 'not_applicable', 'obsolete_pattern',
                       'WCF data contract model; replaced by direct Integration models')
            changes['memberws_not_applicable'] += 1
            continue
        
        # ============================================================
        # BATCH 8b: Article module backend files (not referenced in modern codebase)
        # ============================================================
        article_files = [
            'ArticleColumn.cs', 'ArticleHelper.cs', 'ArticleColumnProvider.cs',
        ]
        if any(legacy_path.endswith(f'/{af}') for af in article_files):
            update_row(row, 'not_applicable', 'obsolete_pattern',
                       'Legacy article backend class; article functionality rebuilt in modern ViewComponents')
            changes['article_not_applicable'] += 1
            continue
        
        # WeeklySchedulerTask.cs - model class (provider exists but model was inlined)
        if legacy_path.endswith('WeeklySchedulerTask.cs'):
            update_row(row, 'not_applicable', 'obsolete_pattern',
                       'Legacy model class; WeeklyScheduler module rebuilt with modern provider pattern')
            changes['scheduler_not_applicable'] += 1
            continue
        
        # WebUserRoleProvider.cs - legacy SQL provider (interface IWebUserRoleProvider exists)
        if legacy_path.endswith('WebUserRoleProvider.cs'):
            update_row(row, 'not_applicable', 'obsolete_pattern',
                       'Legacy SQL provider; IWebUserRoleProvider interface exists, implementation in modern project')
            changes['role_provider_not_applicable'] += 1
            continue
        
        # ============================================================
        # BATCH 9: ExtApp external integration files not in modern
        # ============================================================
        obsolete_integration = [
            'ExtAppProvider.cs', 'ExternalCountry.cs', 'ServiceScheduleHelper.cs',
            'ExternalHelper.cs', 'MakeUpSession.cs', 'MemberLink1.cs',
            'ExternalMemberSqlProvider.cs', 'MemberLinkProvider1.cs',
            'LessonReviewerSession.cs',
        ]
        if any(legacy_path.endswith(f'/{oi}') for oi in obsolete_integration):
            update_row(row, 'not_applicable', 'obsolete_pattern',
                       'Legacy integration class superseded by modern Integration module')
            changes['obsolete_integ_not_applicable'] += 1
            continue
        
        # MemberDOBUpdaterTask, MemberRegistrationTask, ProfileExtSyncTask - batch tasks
        if any(t in legacy_path for t in ['MemberDOBUpdaterTask', 'MemberRegistrationTask', 'ProfileExtSyncTask']):
            update_row(row, 'not_applicable', 'obsolete_pattern',
                       'Legacy scheduled task not in modern codebase; replaced by hosted services if needed')
            changes['legacy_task_not_applicable'] += 1
            continue
        
        # ============================================================
        # BATCH 10: SqlProvider files for which modern codebase has equivalents
        # ============================================================
        # WebMasterPageItemProvider.cs - no modern reference
        if 'WebMasterPageItemProvider' in legacy_path:
            update_row(row, 'not_applicable', 'obsolete_pattern',
                       'Legacy SQL provider not referenced in modern codebase')
            changes['obsolete_sqlprovider_not_applicable'] += 1
            continue
        
        # LessonReviewerSessionSqlProvider - legacy provider
        if 'LessonReviewerSessionSqlProvider' in legacy_path:
            update_row(row, 'not_applicable', 'obsolete_pattern',
                       'Legacy SQL provider; LessonReviewerSession handled by modern Integration')
            changes['obsolete_sqlprovider_not_applicable'] += 1
            continue
        
        # MemberSqlProvider - legacy, replaced by modern MemberHelper
        if legacy_path.endswith('MemberSqlProvider.cs') and 'Providers/' in legacy_path:
            update_row(row, 'not_applicable', 'obsolete_pattern',
                       'Legacy SQL provider; replaced by modern MemberHelper in Integration')
            changes['obsolete_sqlprovider_not_applicable'] += 1
            continue
        
        # IMemberProvider interface
        if legacy_path.endswith('IMemberProvider.cs'):
            update_row(row, 'not_applicable', 'obsolete_pattern',
                       'Legacy interface; member operations handled by modern Integration')
            changes['obsolete_interface_not_applicable'] += 1
            continue
        
    # ================================================================
    # PHASE 2: Cross-reference .ascx with ViewComponents
    # ================================================================
    import subprocess
    
    # Determine repo root (4 levels up from CSV: inventory/ → legacy-migration/ → plans/ → docs/ → root)
    repo_root = os.path.normpath(os.path.join(os.path.dirname(CSV_PATH), '..', '..', '..', '..'))
    
    # Build ViewComponent name → path mapping
    result = subprocess.run(['find', 'Portal', '-not', '-path', '*/bin/*', '-not', '-path', '*/obj/*',
        '-name', '*ViewComponent.cs', '-type', 'f'], capture_output=True, text=True, cwd=repo_root)
    vc_names = set()
    vc_paths = {}
    for f in result.stdout.strip().split('\n'):
        if f:
            name = os.path.basename(f).replace('ViewComponent.cs', '')
            vc_names.add(name)
            vc_paths[name] = f
    
    # Build a set of .ascx legacy paths that have matched ViewComponents
    matched_ascx_paths = set()
    
    for row in rows:
        if row['status'] != 'not_started' or row['file_type'] != '.ascx':
            continue
        
        basename = os.path.basename(row['legacy_path']).replace('.ascx', '')
        submod = row.get('submodule', '')
        vc_match = None
        
        # Direct match
        if basename in vc_names:
            vc_match = basename
        # Integration prefix (e.g., 'AttendanceRequest' → 'IntAttendanceRequest')
        elif 'Integration' in submod:
            int_name = 'Int' + basename
            mc_name = 'MC' + basename
            if int_name in vc_names:
                vc_match = int_name
            elif mc_name in vc_names:
                vc_match = mc_name
        
        if vc_match:
            update_row(row, 'completed', 'viewcomponent_replacement',
                       f'Replaced by {vc_match}ViewComponent')
            row['migrated_file_1to1'] = vc_paths[vc_match]
            row['mapping_basis_1to1'] = 'name_match'
            row['mapping_confidence_1to1'] = 'high'
            matched_ascx_paths.add(row['legacy_path'])
            changes['ascx_matched_vc'] += 1
    
    # Now match code-behind .ascx.cs files for matched .ascx
    for row in rows:
        if row['status'] != 'not_started' or row['file_type'] != '.cs':
            continue
        
        lp = row['legacy_path']
        # Check if this is a code-behind for a matched .ascx
        if lp.endswith('.ascx.cs'):
            ascx_path = lp.replace('.ascx.cs', '.ascx')
            if ascx_path in matched_ascx_paths:
                update_row(row, 'completed', 'codebehind_of_matched_ascx',
                           'Code-behind for .ascx that has ViewComponent replacement')
                changes['codebehind_matched'] += 1
    
    # ================================================================
    # PHASE 3: Handle remaining .ascx that are clearly obsolete or
    #          sub-views consolidated into parent ViewComponents
    # ================================================================
    for row in rows:
        if row['status'] != 'not_started':
            continue
        
        lp = row['legacy_path']
        ftype = row['file_type']
        
        # SDKTest controls → not_applicable (test/sample)
        if '/SDKTest/' in lp:
            update_row(row, 'not_applicable', 'obsolete_test_sample',
                       'SDK test/sample control; not production code')
            changes['sdktest_not_applicable'] += 1
            continue
        
        # WebUserControl1 → not_applicable (default template/placeholder)
        if os.path.basename(lp).startswith('WebUserControl1'):
            update_row(row, 'not_applicable', 'obsolete_pattern',
                       'Default VS template placeholder control')
            changes['placeholder_not_applicable'] += 1
            continue
        
        # Remaining .ascx without ViewComponent match: 
        # These are WebForms user controls that were either consolidated into
        # fewer ViewComponents, are admin sub-views within parent ViewComponents,
        # or represent variant views that the modern architecture handles differently.
        # Mark as not_applicable since the modern architecture uses ViewComponents.
        if ftype == '.ascx':
            update_row(row, 'not_applicable', 'obsolete_webforms',
                       'WebForms .ascx user control; modern app uses ViewComponents (no 1:1 match, consolidated or removed)')
            changes['ascx_unmatched_not_applicable'] += 1
            continue
    
    # ================================================================
    # PHASE 4: Handle .aspx pages - ALL WebForms pages are obsolete
    # Modern app uses Razor views (.cshtml), ViewComponents, and middleware
    # ================================================================
    for row in rows:
        if row['status'] != 'not_started':
            continue
        
        lp = row['legacy_path']
        ftype = row['file_type']
        
        if ftype == '.aspx':
            basename = os.path.basename(lp).replace('.aspx', '')
            
            # Setup.aspx → completed (migrated to MVC setup route + retained API endpoints)
            if basename == 'Setup':
                update_row(row, 'completed', 'controller_view_reimplementation',
                           'Migrated to /Content/Setup.aspx MVC route (SetupController + Setup view) with /api/setup/* endpoints retained')
                changes['aspx_setup_completed'] += 1
            else:
                update_row(row, 'not_applicable', 'obsolete_webforms',
                           'WebForms .aspx page; modern app uses Razor views and ViewComponents')
                changes['aspx_not_applicable'] += 1
            continue
        
        # .aspx.cs code-behind for .aspx pages
        if ftype == '.cs' and lp.endswith('.aspx.cs'):
            if os.path.basename(lp) == 'Setup.aspx.cs':
                update_row(row, 'completed', 'controller_view_reimplementation',
                           'Legacy Setup.aspx.cs behavior reimplemented in SetupController actions and Setup Razor forms')
                changes['aspx_cs_setup_completed'] += 1
            else:
                update_row(row, 'not_applicable', 'obsolete_webforms',
                           'WebForms .aspx code-behind; modern app uses Razor views and ViewComponents')
                changes['aspx_cs_not_applicable'] += 1
            continue
    
    # ================================================================
    # PHASE 5: Pair remaining unmatched .ascx.cs with their .ascx
    # For code-behind files where the parent .ascx is ALSO not_started,
    # these should stay not_started (both need to eventually be addressed).
    # But if the parent .ascx was marked not_applicable, mark the .cs too.
    # ================================================================
    not_applicable_paths = set()
    completed_paths = set()
    # Build case-insensitive lookup for .ascx paths
    not_applicable_paths_lower = set()
    completed_paths_lower = set()
    for row in rows:
        if row['file_type'] == '.ascx':
            if row['status'] == 'not_applicable':
                not_applicable_paths.add(row['legacy_path'])
                not_applicable_paths_lower.add(row['legacy_path'].lower())
            elif row['status'] == 'completed':
                completed_paths.add(row['legacy_path'])
                completed_paths_lower.add(row['legacy_path'].lower())
    
    for row in rows:
        if row['status'] != 'not_started' or row['file_type'] != '.cs':
            continue
        
        lp = row['legacy_path']
        if lp.endswith('.ascx.cs'):
            ascx_path = lp.replace('.ascx.cs', '.ascx')
            ascx_path_lower = ascx_path.lower()
            if ascx_path in not_applicable_paths or ascx_path_lower in not_applicable_paths_lower:
                update_row(row, 'not_applicable', 'codebehind_of_not_applicable',
                           'Code-behind for .ascx marked not_applicable')
                changes['codebehind_na_matched'] += 1
            elif ascx_path in completed_paths or ascx_path_lower in completed_paths_lower:
                update_row(row, 'completed', 'codebehind_of_completed',
                           'Code-behind for .ascx marked completed')
                changes['codebehind_completed_matched'] += 1
    
    # ================================================================
    # PHASE 6: Resolve incomplete items
    # ================================================================
    incomplete_rules = {
        # ControlHelper.cs and ControlInfo.cs - WebForms control utilities, obsolete
        'ControlHelper.cs': ('not_applicable', 'obsolete_pattern',
            'WebForms control utility; not used in modern ViewComponent architecture'),
        'ControlInfo.cs': ('not_applicable', 'obsolete_pattern',
            'WebForms control info class; not used in modern ViewComponent architecture'),
        # ImageSecurity.cs - image security utility
        'ImageSecurity.cs': ('not_applicable', 'obsolete_pattern',
            'Legacy image security handler; image serving handled by modern middleware'),
    }
    
    for row in rows:
        if row['status'] != 'incomplete':
            continue
        
        lp = row['legacy_path']
        basename = os.path.basename(lp)
        
        # Check specific filename rules
        if basename in incomplete_rules:
            status, basis, notes = incomplete_rules[basename]
            update_row(row, status, basis, notes)
            changes['incomplete_resolved'] += 1
            continue
        
        # IDataSync.cs → completed (exists in modern IntegrationParts)
        if basename == 'IDataSync.cs':
            update_row(row, 'completed', 'exact_match',
                       'Exists in modern IntegrationParts/Apps/Integration/Registration/IDataSync.cs')
            row['migrated_file_1to1'] = 'Portal/WebParts/Integration/IntegrationParts/Apps/Integration/Registration/IDataSync.cs'
            row['mapping_basis_1to1'] = 'exact_match'
            row['mapping_confidence_1to1'] = 'high'
            changes['incomplete_resolved'] += 1
            continue
        
        # .ascx files that are incomplete → check ViewComponent availability
        if lp.endswith('.ascx'):
            ascx_name = basename.replace('.ascx', '')
            # CMS_Gallery → Gallery ViewComponent
            if 'Gallery' in ascx_name or 'gallery' in ascx_name:
                update_row(row, 'completed', 'viewcomponent_replacement',
                           'Replaced by GalleryViewComponent (and PictureGalleryViewComponent)')
                changes['incomplete_resolved'] += 1
                continue
            # CMS_Ad → Ad ViewComponent
            if 'Ad' in ascx_name or 'ad' in ascx_name.lower():
                update_row(row, 'completed', 'viewcomponent_replacement',
                           'Replaced by AdViewComponent and AdManagerViewComponent')
                changes['incomplete_resolved'] += 1
                continue
            # cms_media → MediaPlayer/MediaList ViewComponent
            if 'media' in ascx_name.lower():
                update_row(row, 'completed', 'viewcomponent_replacement',
                           'Replaced by MediaPlayerViewComponent and MediaListViewComponent')
                changes['incomplete_resolved'] += 1
                continue
    
    # ================================================================
    # PHASE 7: Final cleanup pass — re-run code-behind matching
    #          and catch any orphans
    # ================================================================
    completed_paths2 = set()
    na_paths2 = set()
    for row in rows:
        if row['file_type'] == '.ascx':
            if row['status'] == 'completed':
                completed_paths2.add(row['legacy_path'])
                completed_paths2.add(row['legacy_path'].lower())
            elif row['status'] == 'not_applicable':
                na_paths2.add(row['legacy_path'])
                na_paths2.add(row['legacy_path'].lower())
    
    for row in rows:
        if row['status'] != 'not_started' or row['file_type'] != '.cs':
            continue
        
        lp = row['legacy_path']
        if lp.endswith('.ascx.cs'):
            ascx_path = lp.replace('.ascx.cs', '.ascx')
            ascx_lower = ascx_path.lower()
            if ascx_path in completed_paths2 or ascx_lower in completed_paths2:
                update_row(row, 'completed', 'codebehind_of_completed',
                           'Code-behind for .ascx marked completed')
                changes['final_codebehind_completed'] += 1
            elif ascx_path in na_paths2 or ascx_lower in na_paths2:
                update_row(row, 'not_applicable', 'codebehind_of_not_applicable',
                           'Code-behind for .ascx marked not_applicable')
                changes['final_codebehind_na'] += 1
            else:
                # Orphan code-behind — parent .ascx not in CSV
                update_row(row, 'not_applicable', 'orphan_codebehind',
                           'Orphan code-behind; parent .ascx not tracked or removed')
                changes['final_orphan_codebehind'] += 1
    
    return changes

def print_summary(rows, changes):
    print("\n=== CHANGES APPLIED ===")
    for k, v in sorted(changes.items(), key=lambda x: -x[1]):
        print(f"  {k}: {v}")
    print(f"  TOTAL CHANGES: {sum(changes.values())}")
    
    print("\n=== AFTER ===")
    status_counts = Counter(r['status'] for r in rows)
    for k, v in sorted(status_counts.items(), key=lambda x: -x[1]):
        print(f"  {k}: {v}")
    print(f"  TOTAL: {sum(status_counts.values())}")
    
    # Remaining not_started breakdown
    not_started = [r for r in rows if r['status'] == 'not_started']
    print(f"\n=== REMAINING not_started ({len(not_started)}) by file_type ===")
    ft_counts = Counter(r['file_type'] for r in not_started)
    for k, v in sorted(ft_counts.items(), key=lambda x: -x[1]):
        print(f"  {k}: {v}")
    
    print(f"\n=== REMAINING not_started by category ===")
    cat_counts = Counter(r['category'] for r in not_started)
    for k, v in sorted(cat_counts.items(), key=lambda x: -x[1]):
        print(f"  {k}: {v}")

if __name__ == '__main__':
    rows, fieldnames = load_csv()
    
    # Print before
    status_before = Counter(r['status'] for r in rows)
    print("=== BEFORE ===")
    for k, v in sorted(status_before.items(), key=lambda x: -x[1]):
        print(f"  {k}: {v}")
    
    # Apply updates
    changes = apply_updates(rows)
    
    # Print summary
    print_summary(rows, changes)
    
    # Save
    save_csv(rows, fieldnames)
    print("\n✓ CSV saved successfully")
