IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObjectColumn_Id]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObjectColumn] DROP CONSTRAINT [DF_WebObjectColumn_Id]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObjectColumn_ObjectId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObjectColumn] DROP CONSTRAINT [DF_WebObjectColumn_ObjectId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObjectColumn_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObjectColumn] DROP CONSTRAINT [DF_WebObjectColumn_Name]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebObjectColumn]') AND type in (N'U'))
DROP TABLE [dbo].[WebObjectColumn]
GO
