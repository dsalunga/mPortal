using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WCMS.Common.Utilities;
using WCMS.Framework.Extensions;
using WCMS.WebSystem.Apps.Integration.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddWcmsFramework();

// EF Core DbContexts
builder.Services.AddDbContext<IntegrationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IntegrationDb")));
builder.Services.AddDbContext<MusicDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MusicDb")));
builder.Services.AddDbContext<ExternalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ExternalDb")));

var app = builder.Build();

ConfigUtil.SetConfiguration(app.Configuration);
PathMapper.Configure(app.Environment.ContentRootPath, app.Environment.WebRootPath);

app.UseStaticFiles();
app.UseRouting();

app.UseWcmsPageResolution();
app.UseWcmsFramework();

app.MapGet("/", () => Results.Ok(new { app = "WCMS.WebSystem.Apps.Integration.WebApp", status = "running", framework = ".NET 10" }));
app.MapGet("/health", () => Results.Ok("ok"));
app.MapRazorPages();
app.MapControllers();

app.Run();
