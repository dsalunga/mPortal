IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_MiddleName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] DROP CONSTRAINT [DF_WebUser_MiddleName]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_LastUpdate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] DROP CONSTRAINT [DF_WebUser_LastUpdate]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_Active]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] DROP CONSTRAINT [DF_WebUser_Active]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_RegCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] DROP CONSTRAINT [DF_WebUser_RegCode]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_DateCreated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] DROP CONSTRAINT [DF_WebUser_DateCreated]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_NewEmail]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] DROP CONSTRAINT [DF_WebUser_NewEmail]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_Email2]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] DROP CONSTRAINT [DF_WebUser_Email2]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_Gender]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] DROP CONSTRAINT [DF_WebUser_Gender]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_NameSuffix]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] DROP CONSTRAINT [DF_WebUser_NameSuffix]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_MobileNumber]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] DROP CONSTRAINT [DF_WebUser_MobileNumber]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_TelephoneNumber]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] DROP CONSTRAINT [DF_WebUser_TelephoneNumber]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_LastLoginDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] DROP CONSTRAINT [DF_WebUser_LastLoginDate]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_StatusText]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] DROP CONSTRAINT [DF_WebUser_StatusText]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_PasswordExpiryDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] DROP CONSTRAINT [DF_WebUser_PasswordExpiryDate]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_PhotoPath]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] DROP CONSTRAINT [DF_WebUser_PhotoPath]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_ProviderId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] DROP CONSTRAINT [DF_WebUser_ProviderId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_Status]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] DROP CONSTRAINT [DF_WebUser_Status]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_MaritalStatus]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] DROP CONSTRAINT [DF_WebUser_MaritalStatus]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_LastLoginFailureDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] DROP CONSTRAINT [DF_WebUser_LastLoginFailureDate]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebUser__LoginFailureCount]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] DROP CONSTRAINT [DF__WebUser__LoginFailureCount]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebUser]') AND type in (N'U'))
DROP TABLE [dbo].[WebUser]
GO
