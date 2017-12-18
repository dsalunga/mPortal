IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPageElement_ObjectId_1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPageElement] DROP CONSTRAINT [DF_WebPageElement_ObjectId_1]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPageElement_UsePartTemplatePath_1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPageElement] DROP CONSTRAINT [DF_WebPageElement_UsePartTemplatePath_1]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPageElement_PublicAccess]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPageElement] DROP CONSTRAINT [DF_WebPageElement_PublicAccess]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPageElement_DateModified]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPageElement] DROP CONSTRAINT [DF_WebPageElement_DateModified]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPageElement_ManagementAccess]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPageElement] DROP CONSTRAINT [DF_WebPageElement_ManagementAccess]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebPageElement]') AND type in (N'U'))
DROP TABLE [dbo].[WebPageElement]
GO
