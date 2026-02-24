using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WCMS.Common.Utilities;
using WCMS.Framework.Extensions;
using WCMS.WebSystem.Apps.BranchLocator.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddWcmsFramework();

// EF Core DbContext
builder.Services.AddDbContext<BranchLocatorDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BranchLocatorDb")));

var app = builder.Build();

ConfigUtil.SetConfiguration(app.Configuration);
PathMapper.Configure(app.Environment.ContentRootPath, app.Environment.WebRootPath);

app.UseStaticFiles();
app.UseRouting();

app.UseWcmsPageResolution();

app.MapGet("/", () => Results.Ok(new { app = "WCMS.WebSystem.WebParts.BranchLocator.WebApp", status = "running", framework = ".NET 10" }));
app.MapGet("/health", () => Results.Ok("ok"));
app.MapRazorPages();
app.MapControllers();

app.Run();
