using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Extensions;

var builder = WebApplication.CreateBuilder(args);

// --- Services ---

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

// CMS framework services (IWSession, IWContext)
builder.Services.AddWcmsFramework();

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

var app = builder.Build();

// --- Initialize CMS ---

var config = app.Configuration;
ConfigUtil.SetConfiguration(config);

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

app.UseStaticFiles();
app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

// CMS page resolution middleware
app.UseWcmsPageResolution();

// --- Endpoints ---

app.MapGet("/", (HttpContext ctx) =>
{
    var page = ctx.Items["ResolvedPage"] as WPage;
    if (page != null)
        return Results.Ok(new { page = page.Name, pageId = page.Id, site = page.Site?.Name });

    return Results.Ok(new
    {
        app = "mPortal CMS",
        status = "running",
        framework = ".NET 10",
        initialized = WebObject.IsInitialized
    });
});

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

app.Run();
