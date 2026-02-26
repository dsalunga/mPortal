using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WCMS.Common.Utilities;
using WCMS.Framework.Extensions;
using WCMS.Framework.Middleware;
using WCMS.WebSystem.Apps.Integration.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddWcmsFramework();

// CMS database provider (SQL Server or PostgreSQL)
builder.Services.AddWcmsDatabase(builder.Configuration);

// EF Core DbContexts — provider-aware
var integrationConn = builder.Configuration.GetConnectionString("IntegrationDb")
    ?? builder.Configuration.GetConnectionString("ConnectionString") ?? string.Empty;
builder.Services.AddDbContext<IntegrationDbContext>(options =>
{
    if (DbHelper.Provider == DatabaseProvider.PostgreSql)
        options.UseNpgsql(integrationConn);
    else
        options.UseSqlServer(integrationConn);
});

var musicConn = builder.Configuration.GetConnectionString("MusicDb")
    ?? builder.Configuration.GetConnectionString("ConnectionString") ?? string.Empty;
builder.Services.AddDbContext<MusicDbContext>(options =>
{
    if (DbHelper.Provider == DatabaseProvider.PostgreSql)
        options.UseNpgsql(musicConn);
    else
        options.UseSqlServer(musicConn);
});

var externalConn = builder.Configuration.GetConnectionString("ExternalDb")
    ?? builder.Configuration.GetConnectionString("ConnectionString") ?? string.Empty;
builder.Services.AddDbContext<ExternalDbContext>(options =>
{
    if (DbHelper.Provider == DatabaseProvider.PostgreSql)
        options.UseNpgsql(externalConn);
    else
        options.UseSqlServer(externalConn);
});

var app = builder.Build();

ConfigUtil.SetConfiguration(app.Configuration);
PathMapper.Configure(app.Environment.ContentRootPath, app.Environment.WebRootPath);
HttpContextHelper.Configure(app.Services.GetRequiredService<Microsoft.AspNetCore.Http.IHttpContextAccessor>());

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseSecurityHeaders();
app.UseStaticFiles();
app.UseRouting();

app.UseWcmsPageResolution();
app.UseWcmsFramework();

app.MapGet("/", () => Results.Ok(new { app = "WCMS.WebSystem.Apps.Integration.WebApp", status = "running", framework = ".NET 10" }));
app.MapGet("/health", () => Results.Ok("ok"));
app.MapRazorPages();
app.MapControllers();

// CMS page fallback endpoint
app.MapCmsPages();

app.Run();
