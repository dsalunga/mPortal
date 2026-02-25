using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
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

builder.Services.AddProblemDetails();
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

// CMS framework services (IWSession, IWContext)
builder.Services.AddWcmsFramework();

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

// Bundling & minification
builder.Services.AddWebOptimizer(pipeline =>
{
    pipeline.MinifyCssFiles("Content/**/*.css");
    pipeline.MinifyJsFiles("Content/**/*.js");
});

var app = builder.Build();

// --- Initialize CMS ---

var config = app.Configuration;
ConfigUtil.SetConfiguration(config);
PathMapper.Configure(app.Environment.ContentRootPath, app.Environment.WebRootPath);

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

app.UseSecurityHeaders();
app.UseWebOptimizer();
app.UseStaticFiles();
app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

// CMS page resolution and rendering pipeline
app.UseWcmsPageResolution();
app.UseWcmsPageRendering();
app.UseWcmsFramework();

// --- Endpoints ---

app.MapGet("/health", () => Results.Ok("ok"));

app.MapGet("/api/system/info", () => Results.Ok(new
{
    app = "mPortal CMS",
    framework = ".NET 10",
    initialized = WebObject.IsInitialized,
    environment = WConfig.Environment
}));

app.MapRazorPages();
app.MapControllers();

// CMS page fallback endpoint — renders pages resolved by PageResolutionMiddleware
app.MapCmsPages();

app.Run();

// Expose Program class for integration testing (WebApplicationFactory<Program>)
public partial class Program { }
