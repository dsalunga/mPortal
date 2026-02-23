using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WCMS.Common.Utilities;
using WCMS.Framework.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddWcmsFramework();

var app = builder.Build();

ConfigUtil.SetConfiguration(app.Configuration);

app.UseStaticFiles();
app.UseRouting();

app.UseWcmsPageResolution();

app.MapGet("/", () => Results.Ok(new { app = "BibleReader.WebApp", status = "running", framework = ".NET 10" }));
app.MapGet("/health", () => Results.Ok("ok"));
app.MapRazorPages();
app.MapControllers();

app.Run();
