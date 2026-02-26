using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Configuration;
using WCMS.Framework.Core;
using WCMS.Framework.Extensions;
using WCMS.Framework.Middleware;

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
    healthChecks.AddNpgSql(defaultConnStr ?? "Host=localhost;Database=mPortal;Username=postgres;Password=${PG_PASSWORD}",
        name: "postgresql", tags: new[] { "db", "sql" });
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

app.MapRazorPages();
app.MapControllers();

// CMS page fallback endpoint — renders pages resolved by PageResolutionMiddleware
app.MapCmsPages();

app.Run();

// Expose Program class for integration testing (WebApplicationFactory<Program>)
public partial class Program { }
