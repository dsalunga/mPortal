# mPortal CMS - Dockerfile
# Multi-stage build for the main web application

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project files for restore
COPY Portal/WebSystem/WCMS.Common/WCMS.Common.csproj Portal/WebSystem/WCMS.Common/
COPY Portal/WebSystem/WCMS.Framework/WCMS.Framework.csproj Portal/WebSystem/WCMS.Framework/
COPY Portal/WebSystem/WCMS.Framework.Core.SqlProvider/WCMS.Framework.Core.SqlProvider.csproj Portal/WebSystem/WCMS.Framework.Core.SqlProvider/
COPY Portal/WebSystem/WCMS.Framework.Core.XmlProvider/WCMS.Framework.Core.XmlProvider.csproj Portal/WebSystem/WCMS.Framework.Core.XmlProvider/
COPY Portal/WebSystem/WCMS.Framework.Core.SqlProvider.Smo/WCMS.Framework.Core.SqlProvider.Smo.csproj Portal/WebSystem/WCMS.Framework.Core.SqlProvider.Smo/
COPY Portal/WebSystem/WCMS.WebSystem.Utilities/WCMS.WebSystem.Utilities.csproj Portal/WebSystem/WCMS.WebSystem.Utilities/
COPY Portal/WebSystem/WCMS.WebSystem.ViewModels/WCMS.WebSystem.csproj Portal/WebSystem/WCMS.WebSystem.ViewModels/
COPY Portal/WebSystem/WebSystem-MVC/WCMS.WebSystem.WebApp.csproj Portal/WebSystem/WebSystem-MVC/
COPY Directory.Build.props ./

RUN dotnet restore Portal/WebSystem/WebSystem-MVC/WCMS.WebSystem.WebApp.csproj

# Copy source and build
COPY Portal/WebSystem/ Portal/WebSystem/
RUN dotnet publish Portal/WebSystem/WebSystem-MVC/WCMS.WebSystem.WebApp.csproj -c Release -o /app/publish --no-restore

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

# Copy static content
COPY Portal/WebSystem/WebSystem-MVC/Content/ Content/

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "WCMS.WebSystem.WebApp.dll"]
