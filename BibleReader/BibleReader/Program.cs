using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () => Results.Ok(new
{
    app = "BibleReader.WebApp",
    status = "modernized-scaffold",
    framework = ".NET 10"
}));

app.MapGet("/health", () => Results.Ok("ok"));

app.Run();
