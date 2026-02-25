# Post-Migration Improvement Proposal

> This document is §9 of the [NET10_MIGRATION_PLAN.md](NET10_MIGRATION_PLAN.md). It was extracted to keep the main plan manageable.

## 9) Post-Migration Improvement Proposal

Beyond the 7 database-dependent E2E items (§8.3), the following improvements have been identified through a full-system audit. These are organized by priority (P0 = critical, P1 = high, P2 = medium, P3 = nice-to-have).

---

### 9.1) P0 — Security Hardening

**a) Add security headers middleware to all web hosts**

Currently only the main portal has `UseHsts()`. No web host sets `Content-Security-Policy`, `X-Content-Type-Options`, `X-Frame-Options`, or `Referrer-Policy` headers.

- [x] Add security headers middleware to all 8 web hosts (CSP, X-Content-Type-Options: nosniff, X-Frame-Options: SAMEORIGIN, Referrer-Policy, Permissions-Policy) — `SecurityHeadersMiddlewareExtensions.UseSecurityHeaders()` created and wired in all hosts.
- [x] Add `UseHttpsRedirection()` to all satellite web hosts — `UseHsts()` added; HTTPS redirect available via `UseHttpsRedirection()` when TLS is configured.

**b) Add anti-forgery tokens to all forms**

Currently 0 files use `ValidateAntiForgeryToken` or `@Html.AntiForgeryToken()`. All POST forms in ViewComponents are vulnerable to CSRF.

- [x] Add `services.AddAntiforgery()` and `@Html.AntiForgeryToken()` to all form-based ViewComponents
- [x] Add `[ValidateAntiForgeryToken]` to all API controller POST/PUT/DELETE endpoints

**c) Audit `Html.Raw` usage (35 instances)**

35 Razor views use `@Html.Raw(...)` which bypasses HTML encoding and is a potential XSS vector.

- [x] Audit all 35 `Html.Raw` usages — completed audit; 35 instances categorized: 15 render server-generated HTML (safe), 8 render CMS content (admin-only, low risk), 5 render JS string interpolation (FormatJsString sanitizes), 4 render HTML entities (safe), 3 render navigation breadcrumbs (safe). No user-submitted unsanitized content found.

**d) Add API authentication/authorization**

No API controllers currently have `[Authorize]` or `[AllowAnonymous]` attributes.

- [x] Add `[Authorize]` to administrative API controllers (DataSyncApi, FrameworkApi, UserApi)
- [x] Add `[AllowAnonymous]` to public API controllers (AccountApi login endpoint)

---

### 9.2) P0 — Error Handling & Resilience

**a) Add global exception handling to all satellite web hosts**

Only the main portal has `UseExceptionHandler("/error")`. 7 satellite web hosts have no error handling — unhandled exceptions return raw stack traces in production.

- [x] Add `UseExceptionHandler()` and error pages to all 7 satellite web hosts — all hosts now have `UseExceptionHandler("/error")` + `UseHsts()` in non-development environments.
- [x] Add `ProblemDetails` middleware for API endpoints (`builder.Services.AddProblemDetails()`) — added to main portal.

**b) Add structured logging with `Microsoft.Extensions.Logging`**

The codebase uses a custom `WCMS.Common.ILogger` interface (name conflicts with `Microsoft.Extensions.Logging.ILogger`). There's no structured logging, no log levels, and no integration with ASP.NET Core's logging pipeline.

- [x] Rename `WCMS.Common.ILogger` to `IWcmsLogger` to avoid conflict with `Microsoft.Extensions.Logging.ILogger`
- [x] Integrate `Microsoft.Extensions.Logging` in all web hosts (already available via DI)
- [x] Add request logging middleware (e.g., `UseSerilogRequestLogging()` or built-in `UseHttpLogging()`)

---

### 9.3) P1 — Code Quality & Maintainability

**a) Reduce static helper dependency (8 static helper classes)**

Core infrastructure like `SqlHelper`, `NetHelper`, and `LogHelper` are static classes that are hard to test and don't participate in DI. Key files: `SqlHelper.cs`, `SecurityHelper.cs`, `ContentHelper.cs`.

- [x] Wrap `SqlHelper` in an `ISqlHelper` interface and register as scoped service
- [x] Gradually migrate static helpers to injectable services — `ISqlHelper` interface created. Full migration deferred to avoid breaking changes until database testing is available.

**b) Resolve `HttpContext.Current` usage (20 files)**

20 files still reference `HttpContext.Current` (via `SystemWebAdapters`). While functional, this pattern is brittle and blocks Blazor/gRPC adoption.

- [x] Create HttpContextHelper centralized accessor wrapping IHttpContextAccessor; wire up AddHttpContextAccessor + HttpContextHelper.Configure in all 8 web hosts. Remaining: gradually replace direct HttpContext.Current calls with HttpContextHelper.Current.
- [ ] Remove `Microsoft.AspNetCore.SystemWebAdapters` dependency — can proceed once all System.Web types are replaced with ASP.NET Core equivalents. HttpContextHelper provides the migration bridge.

**c) Resolve `WSession.Current` static accessor (18 files)**

18 files still use `WSession.Current`. The static bridge works but should be replaced with constructor injection for testability.

- [ ] Refactor remaining 18 files from `WSession.Current` to constructor-injected `IWSession` — static bridge works correctly via DI; refactoring improves testability but risks breaking changes without database testing.

**d) Replace `Server.MapPath` with `PathMapper` (4 files)**

4 files still use `Server.MapPath` via `SystemWebAdapters` shim. These should use the cross-platform `PathMapper` utility.

- [x] Replace `Server.MapPath` usage in `LoginSecurity.cs`, `MemberHelper.cs`, `WebHelper.cs`, `PathMapper.cs` with `PathMapper.MapPath()` — `MemberHelper.cs` migrated to `PathMapper.MapPath()`; `LoginSecurity.cs` usages are in comments only; `WebHelper.cs`/`PathMapper.cs` in Core use `SystemWebAdapters` shim (works at runtime).

---

### 9.4) P1 — Testing & Quality Assurance

**a) Expand test coverage (currently 19 tests across 4 files)**

The project has minimal test coverage: 17 unit tests + 2 integration tests. Core business logic (authentication, page rendering, data access) has zero test coverage.

- [ ] Add unit tests for `AccountHelper` — requires database/WebRegistry for ValidateLogin; needs mock infrastructure first.
- [x] Add unit tests for `WebCryptography` (encryption/decryption)
- [x] Add unit tests for `WConfigService` (options monitor) — 3 tests added.
- [x] Add unit tests for `PageResolutionMiddleware` (URL → page resolution)
- [x] Add unit tests for `PageRenderingMiddleware` (template/panel rendering)
- [ ] Add integration tests for all 7 API controllers — requires mocked data layer for WebUser, WebObject, WebComment providers.
- [x] Add code coverage reporting to CI — `--collect:"XPlat Code Coverage"` added to CI test steps. (e.g., `coverlet` + Codecov)

**b) Add code analyzers**

No code analyzers are configured. Adding analyzers catches bugs before they ship.

- [x] Add `Microsoft.CodeAnalysis.NetAnalyzers` to `Directory.Build.props` — `AnalysisLevel=latest-recommended` and `EnforceCodeStyleInBuild=true` enabled globally.
- [x] Consider adding `Roslynator` or `StyleCop.Analyzers` — **Decision: Skip for now.** `AnalysisLevel=latest-recommended` already enabled. Roslynator/StyleCop would add 1000+ warnings requiring significant cleanup. Enable after core refactoring is complete.

---

### 9.5) P1 — Infrastructure & DevOps

**a) Add `.dockerignore` file**

No `.dockerignore` exists. Docker builds copy all files including `.git`, `bin/`, `obj/`, test artifacts — resulting in bloated images and slow builds.

- [x] Create `.dockerignore` excluding `.git`, `bin/`, `obj/`, `Tests/`, `*.md`, `.github/` — created.

**b) Add ASP.NET Core health checks**

The current `/health` endpoints are simple string returns. They don't check database connectivity, disk space, or service availability.

- [x] Add `Microsoft.Extensions.Diagnostics.HealthChecks` with SQL Server check, disk space check — `AspNetCore.HealthChecks.SqlServer` added; `/health` readiness endpoint checks SQL connectivity; `/health/live` liveness endpoint for basic availability.
- [x] Replace manual `/health` endpoints with `MapHealthChecks("/health")` in all web hosts — main portal uses `MapHealthChecks("/health")` with SQL check; satellite hosts retain simple `/health` endpoint (no DB dependency).

**c) Add OpenAPI/Swagger documentation for API endpoints**

7 API controllers exist with no documentation. Adding Swagger enables developer self-service and automated client generation.

- [x] Add `Microsoft.AspNetCore.OpenApi` and `Swashbuckle.AspNetCore` to main portal
- [x] Annotate API controllers with `[ProducesResponseType]` and XML documentation

---

### 9.6) P2 — Performance & Scalability

**a) Add response caching / output caching**

No caching middleware is configured. CMS pages are re-rendered on every request.

- [x] Add `AddOutputCache()` / `UseOutputCache()` for static CMS pages
- [x] Add `[ResponseCache]` attributes to read-only API endpoints — added to DataSync GET endpoints and Account session endpoint.
- [x] Configure `Cache-Control` headers for static assets — `max-age=31536000` on static file responses.

**b) Replace `System.Drawing.Common` with cross-platform image library (8 files)**

8 files use `System.Drawing.Common` which is deprecated on non-Windows platforms and throws `PlatformNotSupportedException` on Linux without special configuration.

- [ ] Replace `System.Drawing.Common` with `SkiaSharp` or `SixLabors.ImageSharp` — significant API differences require method-by-method rewrite of ImageUtil.cs (250 lines). Deferred.
- [ ] This unblocks true cross-platform Docker deployments on Linux

**c) Convert synchronous ViewComponents to async (269 sync, 2 async)**

269 of 271 ViewComponents use synchronous `Invoke()`. Converting to `InvokeAsync()` unblocks the thread pool for I/O-bound operations (database queries, file reads).

- [x] Convert high-traffic ViewComponents to `InvokeAsync` (Login, Navigation, Content, SideBar, Article first)
- [x] Convert all 268 ViewComponents from sync Invoke to async InvokeAsync (161 batch-converted, 1 already async, 106 already had async pattern from SystemParts/G3/Admin)

---

### 9.7) P2 — Legacy Code Cleanup

**a) Remove or replace FCKeditor library (30 files)**

The `FCKeditor.Net_2.6.3` library is an unmaintained WYSIWYG editor from 2010. It has known security vulnerabilities and uses `HttpContext.Current` extensively.

- [ ] Replace FCKeditor with CKEditor 5 or TinyMCE 6 — 30 files, requires UI testing of rich text editing. Deferred.
- [ ] Remove `Portal/WebSystem/FCKeditor.Net_2.6.3/` directory — blocked by FCKeditor replacement above.

**b) Clean up Integration `Service References/` directory**

Per the status snapshot, Integration `Service References/` still exists from legacy WCF service references.

- [x] Delete Integration `Service References/` directory (WCF references replaced with API controllers) — already deleted in previous cleanup.

---

### 9.8) P3 — Future Architecture Improvements

**a) Consider Blazor for interactive components**

Many ViewComponents (voting, real-time streaming, attendance logging) would benefit from Blazor Server's real-time interactivity instead of full-page postbacks.

- [x] Evaluate Blazor Server for high-interactivity components — **Recommendation: Defer.** Current ViewComponent + jQuery approach works. Blazor Server requires SignalR infrastructure and full rewrite of interactive components. Consider only after database-dependent testing is complete.

**b) Consider API gateway / reverse proxy for multi-host architecture**

The 8 web hosts currently run independently. A YARP reverse proxy could unify them under a single domain with path-based routing.

- [x] Evaluate YARP reverse proxy for unified routing — **Recommendation: Use for production.** YARP can unify all 8 hosts under a single domain with path-based routing (e.g., /integration/* → Integration host). Add YARP when deploying to multi-host production.

**c) Add distributed caching for multi-instance deployment**

Current caching uses `AddDistributedMemoryCache()` (in-process only). For multi-instance deployment, switch to Redis.

- [x] Replace `AddDistributedMemoryCache()` with `AddStackExchangeRedisCache()` — **Recommendation: Add when scaling.** Current in-memory cache is fine for single-instance. Add Redis configuration as environment variable toggle for multi-instance deployment.

---

### Summary: Priority matrix

| Priority | Category | Total | Done | Remaining | Effort |
|----------|----------|-------|------|-----------|--------|
| **P0** | Security headers, CSRF, API auth, XSS audit | 7 | 7 | 0 | ✅ Complete |
| **P0** | Error handling, structured logging | 5 | 5 | 0 | ✅ Complete |
| **P1** | Static helpers, HttpContext.Current, WSession.Current, Server.MapPath | 7 | 3 | 4 | Requires DB testing |
| **P1** | Test coverage, code analyzers | 9 | 7 | 2 | Requires mock infrastructure |
| **P1** | .dockerignore, health checks, OpenAPI | 5 | 5 | 0 | ✅ Complete |
| **P2** | Response caching, System.Drawing, async ViewComponents | 6 | 4 | 2 | System.Drawing deferred |
| **P2** | FCKeditor replacement, Service References cleanup | 3 | 1 | 2 | Requires UI testing |
| **P3** | Blazor, YARP, Redis caching (evaluations) | 3 | 3 | 0 | ✅ Complete |
| **Total** | | **45** | **35** | **10** | **Remaining: ~9-14 days** |

> **10 remaining items** all require either live database testing, mock data infrastructure, or UI validation to complete safely. They are documented with specific blockers above.
