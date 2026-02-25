using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WCMS.BibleReader.Core;
using WCMS.BibleReader.Core.Providers;
using WCMS.Common.Utilities;
using WCMS.Framework.Extensions;
using WCMS.Framework.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddWcmsFramework();

// BibleReader.Core services
builder.Services.AddSingleton<BibleManager>();
builder.Services.AddSingleton<BibleVersionProvider>();
builder.Services.AddSingleton<BibleBookNameProvider>();
builder.Services.AddSingleton<BibleVersionLanguageProvider>();
builder.Services.AddSingleton<GenericBibleVerseProvider>();

var app = builder.Build();

ConfigUtil.SetConfiguration(app.Configuration);
PathMapper.Configure(app.Environment.ContentRootPath, app.Environment.WebRootPath);

app.UseStaticFiles();
app.UseRouting();

app.UseWcmsPageResolution();
app.UseWcmsFramework();

app.MapGet("/", () => Results.Ok(new { app = "BibleReader.WebApp", status = "running", framework = ".NET 10" }));
app.MapGet("/health", () => Results.Ok("ok"));
app.MapRazorPages();
app.MapControllers();

// CMS page fallback endpoint
app.MapCmsPages();

app.Run();
