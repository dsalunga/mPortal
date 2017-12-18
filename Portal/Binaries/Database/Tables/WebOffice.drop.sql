IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebOffice_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebOffice] DROP CONSTRAINT [DF_WebOffice_Name]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebOffice_ParentId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebOffice] DROP CONSTRAINT [DF_WebOffice_ParentId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebOffice_AddressLine1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebOffice] DROP CONSTRAINT [DF_WebOffice_AddressLine1]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebOffice_MobileNumber]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebOffice] DROP CONSTRAINT [DF_WebOffice_MobileNumber]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebOffice_PhoneNumber]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebOffice] DROP CONSTRAINT [DF_WebOffice_PhoneNumber]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebOffice_EmailAddress]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebOffice] DROP CONSTRAINT [DF_WebOffice_EmailAddress]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebOffice_ContactPersonId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebOffice] DROP CONSTRAINT [DF_WebOffice_ContactPersonId]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebOffice]') AND type in (N'U'))
DROP TABLE [dbo].[WebOffice]
GO
