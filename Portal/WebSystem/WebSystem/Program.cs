using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Configuration;
using WCMS.Framework.Core;
using WCMS.Framework.Extensions;
using WCMS.Framework.Middleware;
using WCMS.WebSystem.Utilities;

var builder = WebApplication.CreateBuilder(args);

// --- Services ---

builder.Services.AddHttpContextAccessor();
builder.Services.AddProblemDetails();
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

// OpenAPI / Swagger documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CMS framework services (IWSession, IWContext)
builder.Services.AddWcmsFramework();

// CMS database provider (SQL Server or PostgreSQL)
builder.Services.AddWcmsDatabase(builder.Configuration);

// CMS configuration options
builder.Services.AddWcmsConfiguration(builder.Configuration);

// Theme support (layout selection based on WebTheme)
builder.Services.AddWcmsThemeSupport();

// Session state
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
        options.ExpireTimeSpan = TimeSpan.FromDays(60);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();
builder.Services.AddAntiforgery();
builder.Services.AddHttpLogging(options => { });

// Output caching for CMS pages
builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(builder => builder.Expire(TimeSpan.FromMinutes(5)));
    options.AddPolicy("StaticAssets", builder => builder.Expire(TimeSpan.FromHours(1)));
});

// Bundling & minification
builder.Services.AddWebOptimizer(pipeline =>
{
    pipeline.MinifyCssFiles("Content/**/*.css");
    pipeline.MinifyJsFiles("Content/**/*.js");
});

// Health checks — provider-aware
var defaultConnStr = builder.Configuration.GetConnectionString("DefaultConnection");
var dbProviderName = builder.Configuration["WCMS:DatabaseProvider"];
var dbProvider = DbHelper.ParseProvider(dbProviderName);

var healthChecks = builder.Services.AddHealthChecks();
if (dbProvider == DatabaseProvider.PostgreSql)
{
    if (!string.IsNullOrWhiteSpace(defaultConnStr))
    {
        healthChecks.AddNpgSql(defaultConnStr, name: "postgresql", tags: new[] { "db", "sql" });
    }
    else
    {
        healthChecks.AddCheck("postgresql",
            () => HealthCheckResult.Unhealthy("Missing ConnectionStrings:DefaultConnection for PostgreSql provider."),
            tags: new[] { "db", "sql" });
    }
}
else
{
    healthChecks.AddSqlServer(defaultConnStr ?? "Server=.;Database=WCMS;Trusted_Connection=True;TrustServerCertificate=True",
        name: "sqlserver", tags: new[] { "db", "sql" });
}

var app = builder.Build();

// --- Initialize CMS ---

var config = app.Configuration;
ConfigUtil.SetConfiguration(config);
PathMapper.Configure(app.Environment.ContentRootPath, app.Environment.WebRootPath);

// Wire up IHttpContextAccessor for legacy static accessors
var httpContextAccessor = app.Services.GetRequiredService<Microsoft.AspNetCore.Http.IHttpContextAccessor>();
HttpContextHelper.Configure(httpContextAccessor);
WebContextBase.ConfigureAccessor(httpContextAccessor);
WSession.Configure(httpContextAccessor);

if (WConfig.AllowCache)
{
    WebObject.Initialize();
    WebObject.LoadCache();
}

// --- Middleware pipeline ---

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "mPortal CMS API v1");
        options.RoutePrefix = "swagger";
    });
}

app.UseSecurityHeaders();
app.UseHttpLogging();
app.UseWebOptimizer();
// Legacy runtime serves most static assets from /Content/**.
var legacyContentPath = Path.Combine(app.Environment.ContentRootPath, "Content");
if (Directory.Exists(legacyContentPath))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(legacyContentPath),
        RequestPath = "/Content",
        OnPrepareResponse = ctx =>
        {
            ctx.Context.Response.Headers["Cache-Control"] = "public,max-age=31536000";
        }
    });

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(legacyContentPath),
        RequestPath = "/content",
        OnPrepareResponse = ctx =>
        {
            ctx.Context.Response.Headers["Cache-Control"] = "public,max-age=31536000";
        }
    });
}

// Keep default static file middleware for wwwroot when present.
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers["Cache-Control"] = "public,max-age=31536000";
    }
});
app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseWcmsSessionHydration();
app.UseAuthorization();
app.UseOutputCache();

// CMS page resolution and rendering pipeline
app.UseWcmsPageResolution();
app.UseWcmsPageRendering();
app.UseWcmsFramework();

// --- Endpoints ---

app.MapHealthChecks("/health");
app.MapGet("/health/live", () => Results.Ok("ok"));

app.MapGet("/api/system/info", () => Results.Ok(new
{
    app = "mPortal CMS",
    framework = ".NET 10",
    initialized = WebObject.IsInitialized,
    environment = WConfig.Environment,
    databaseProvider = DbHelper.Provider.ToString()
}));

if (app.Environment.IsDevelopment())
{
    app.MapGet("/api/system/diag", () =>
    {
        string fullError(Exception ex) { var msg = $"{ex.GetType().Name}: {ex.Message}"; var inner = ex.InnerException; while (inner != null) { msg += $" -> {inner.GetType().Name}: {inner.Message}"; inner = inner.InnerException; } return msg; }
        var diag = new System.Collections.Generic.Dictionary<string, object>();
        try { diag["webObjectInit"] = WebObject.IsInitialized; } catch (Exception ex) { diag["webObjectInit"] = fullError(ex); }
        try { diag["dbProvider"] = DbHelper.Provider.ToString(); } catch (Exception ex) { diag["dbProvider"] = fullError(ex); }
        try { diag["webObjectCount"] = WebObject.GetList() != null ? System.Linq.Enumerable.Count(WebObject.GetList()) : -1; } catch (Exception ex) { diag["webObjectCount"] = fullError(ex); }
        try { diag["siteIdentityProvider"] = WebSiteIdentity.Provider?.GetType().FullName ?? "null"; } catch (Exception ex) { diag["siteIdentityProvider"] = fullError(ex); }
        try { var ids = WebSiteIdentity.Provider?.GetList(); diag["siteIdentities"] = ids != null ? System.Linq.Enumerable.Count(ids) : -1; } catch (Exception ex) { diag["siteIdentities"] = fullError(ex); }
        try { diag["siteProvider"] = WSite.Provider?.GetType().FullName ?? "null"; } catch (Exception ex) { diag["siteProvider"] = fullError(ex); }
        try { var sl = WSite.GetList(); diag["sites"] = sl != null ? System.Linq.Enumerable.Count(sl) : -1; } catch (Exception ex) { diag["sites"] = fullError(ex); }
        try { diag["defaultSite"] = WConfig.DefaultSite?.Name ?? "null"; } catch (Exception ex) { diag["defaultSite"] = fullError(ex); }
        try { var site = WConfig.DefaultSite; diag["homePage"] = site?.HomePage?.Name ?? "null"; } catch (Exception ex) { diag["homePage"] = fullError(ex); }
        try { diag["systemNode"] = WConfig.SystemNode?.Key ?? "null"; } catch (Exception ex) { diag["systemNode"] = fullError(ex); }
        return Results.Ok(diag);
    });
}

app.MapRazorPages();
app.MapControllers();

// --- Setup / Database Management API (Development only) ---
if (app.Environment.IsDevelopment())
{
    var setupGroup = app.MapGroup("/api/setup").WithTags("Setup");
    var setupApiKey = app.Configuration["WCMS:SetupApiKey"];

    setupGroup.AddEndpointFilter(async (context, next) =>
    {
        var remoteIp = context.HttpContext.Connection.RemoteIpAddress;
        var isLoopback = remoteIp != null &&
                         (IPAddress.IsLoopback(remoteIp) ||
                          (remoteIp.IsIPv4MappedToIPv6 && IPAddress.IsLoopback(remoteIp.MapToIPv4())));
        if (!isLoopback)
            return Results.StatusCode(StatusCodes.Status403Forbidden);

        if (!string.IsNullOrWhiteSpace(setupApiKey))
        {
            var providedKey = context.HttpContext.Request.Headers["X-WCMS-Setup-Key"].ToString();
            if (string.IsNullOrWhiteSpace(providedKey))
                return Results.Unauthorized();

            var expectedBytes = Encoding.UTF8.GetBytes(setupApiKey);
            var providedBytes = Encoding.UTF8.GetBytes(providedKey);
            var isValid = expectedBytes.Length == providedBytes.Length &&
                          CryptographicOperations.FixedTimeEquals(expectedBytes, providedBytes);
            if (!isValid)
                return Results.Unauthorized();
        }

        return await next(context);
    });

    setupGroup.MapGet("/inspect", () =>
    {
        var db = new DbManager();
        var report = new List<string>();

        report.Add($"DatabaseProvider: {DbHelper.Provider}");
        report.Add($"XmlPath: {db.XML_PATH}");
        report.Add($"BackupPath: {db.BackupPath}");

        // Check database connectivity
        try
        {
            using var r = DbHelper.ExecuteReader(CommandType.Text, "SELECT 1");
            report.Add("Database connection: OK");
        }
        catch (Exception ex)
        {
            report.Add($"Database connection FAILED: {ex.Message}");
            return Results.Ok(new { status = "error", report });
        }

        // Check XML seed data
        string xmlFile = Path.Combine(db.XML_PATH, DbConstants.XML_FILE);
        report.Add(File.Exists(xmlFile)
            ? $"XML definition ({DbConstants.XML_FILE}): OK"
            : $"XML definition ({DbConstants.XML_FILE}): MISSING");

        // Check tables
        try
        {
            var items = WebObject.GetList();
            if (items != null)
            {
                foreach (var item in items)
                {
                    try
                    {
                        var quotedName = DbSyntax.QuoteIdentifier(item.Name);
                        using var r2 = DbHelper.ExecuteReader(CommandType.Text,
                            $"SELECT * FROM {quotedName} LIMIT 1");
                        report.Add($"Table {item.Name}: OK");
                    }
                    catch
                    {
                        try
                        {
                            // SQL Server uses TOP instead of LIMIT
                            using var r3 = DbHelper.ExecuteReader(CommandType.Text,
                                $"SELECT TOP 1 * FROM {item.Name}");
                            report.Add($"Table {item.Name}: OK");
                        }
                        catch (Exception ex2)
                        {
                            report.Add($"Table {item.Name}: MISSING or ERROR — {ex2.Message}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            report.Add($"WebObject list error: {ex.Message}");
        }

        return Results.Ok(new { status = "ok", report });
    });

    setupGroup.MapGet("/objects", () =>
    {
        try
        {
            var objects = from item in WebObject.GetList()
                          select new
                          {
                              item.Id,
                              item.Name,
                              item.IdentityColumn,
                              item.LastRecordId,
                              item.DateModified,
                              Count = SafeGetCount(item)
                          };
            return Results.Ok(objects);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }

        static int SafeGetCount(WebObject item)
        {
            try { return WebObject.GetCount(item); } catch { return -1; }
        }
    });

    setupGroup.MapPost("/backup", () =>
    {
        var db = new DbManager();
        var log = new List<string>();
        db.Backup(msg => log.Add(msg));
        return Results.Ok(new { status = "completed", log });
    });

    setupGroup.MapPost("/restore", () =>
    {
        var db = new DbManager();
        var log = new List<string>();
        db.Restore(msg => log.Add(msg));
        return Results.Ok(new { status = "completed", log });
    });

    setupGroup.MapPost("/restore/{objectName}", (string objectName) =>
    {
        var db = new DbManager();
        var log = new List<string>();

        var item = WebObject.GetList()?.FirstOrDefault(o =>
            string.Equals(o.Name, objectName, StringComparison.OrdinalIgnoreCase));
        if (item == null)
            return Results.NotFound(new { error = $"WebObject '{objectName}' not found." });

        try
        {
            db.RestoreObjectData(item, msg => log.Add(msg));
            return Results.Ok(new { status = "completed", objectName = item.Name, log });
        }
        catch (Exception ex)
        {
            log.Add(ex.ToString());
            return Results.Ok(new { status = "error", objectName = item.Name, log });
        }
    });

    setupGroup.MapPost("/restore-selected", (SetupRestoreSelectedRequest request) =>
    {
        var db = new DbManager();
        var log = new List<string>();

        foreach (var objectName in request.ObjectNames)
        {
            var item = WebObject.GetList()?.FirstOrDefault(o =>
                string.Equals(o.Name, objectName, StringComparison.OrdinalIgnoreCase));
            if (item == null)
            {
                log.Add($"WebObject '{objectName}' not found, skipping.");
                continue;
            }

            try
            {
                if (request.RestoreSchema)
                {
                    db.DropObjectSchema(item, msg => log.Add(msg));
                    db.RestoreObjectSchema(item);
                }

                db.RestoreObjectData(item, msg => log.Add(msg));
            }
            catch (Exception ex)
            {
                log.Add($"Error restoring {item.Name}: {ex.Message}");
            }
        }

        return Results.Ok(new { status = "completed", log });
    });

    setupGroup.MapPost("/drop", () =>
    {
        var db = new DbManager();
        var log = new List<string>();
        db.DropAllObjects(msg => log.Add(msg));
        return Results.Ok(new { status = "completed", log });
    });

    setupGroup.MapPost("/reset", (IHostApplicationLifetime lifetime) =>
    {
        lifetime.StopApplication();
        return Results.Ok(new { status = "reset", message = "Application is shutting down for restart." });
    });

    setupGroup.MapPost("/create-database", () =>
    {
        var db = new DbManager();
        if (db.CheckCreateDatabase())
            return Results.Ok(new { status = "created", message = "Database created successfully." });
        else
            return Results.Ok(new { status = "skipped", message = "Database already exists (or not supported on this provider)." });
    });
}

// CMS page fallback endpoint — resolves unmatched paths through CMS rendering.
app.MapControllerRoute(
    name: "CmsFallback",
    pattern: "{**slug}",
    defaults: new { controller = "Cms", action = "Render" });

app.Run();

// Expose Program class for integration testing (WebApplicationFactory<Program>)
public partial class Program { }

// Record type for restore-selected request
record SetupRestoreSelectedRequest(string[] ObjectNames, bool RestoreSchema = false);
