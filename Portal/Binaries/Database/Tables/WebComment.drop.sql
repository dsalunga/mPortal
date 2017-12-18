IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebComment_ParentId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebComment] DROP CONSTRAINT [DF_WebComment_ParentId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebCommen__UserName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebComment] DROP CONSTRAINT [DF__WebCommen__UserName]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebCommen__UserEmail]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebComment] DROP CONSTRAINT [DF__WebCommen__UserEmail]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebComment]') AND type in (N'U'))
DROP TABLE [dbo].[WebComment]
GO
