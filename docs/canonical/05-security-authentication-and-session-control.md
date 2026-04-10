# 05 - Security Authentication And Session Control

## Security model layers
- Object-level authorization via `PublicSecurableObject` and `WebObjectSecurity` entries.
- Session model via custom `WSession` + `UserSessionManager` tracking browser sessions.
- Login cookie validation via `LoginCookieManager`.
- Central/admin UI authorization checks in loader flows (`WHelper.CheckCentralLoaderAccess`).

## Access enforcement
- Pages/sites/master pages support inherit/account/ip-address-based public access logic.
- CMS edit/manage rights derive from object permissions and management access flags.
- Session propagation across domains can occur via `SessionId` query key (with checks).

## Crypto and credential handling observations
- Password hash helper uses MD5-based hash composition (`SecurityHelper.CreatePasswordHash`).
- `AccountHelper.ValidateLogin` compares provided string directly with stored password value in common paths.
- Legacy RSA + Rijndael client-side exchange patterns exist in login/change-password controls.

## Key anchors
- `legacy/Portal/WebSystem/WCMS.Framework/WSession.cs`
- `legacy/Portal/WebSystem/WCMS.Framework/Security/PublicSecurableObject.cs`
- `legacy/Portal/WebSystem/WCMS.Framework/Utilities/AccountHelper.cs`
- `legacy/Portal/WebSystem/WCMS.Framework/Utilities/SecurityHelper.cs`
- `legacy/Portal/WebSystem/WebSystem-MVC/Content/Parts/Central/Controls/ChangePasswordForm.ascx.cs`

## Evaluation
Strength: fine-grained authorization model integrated into core object graph.
Risk: credential and cryptography approach is legacy-grade and should be treated as high-priority modernization/security debt.
