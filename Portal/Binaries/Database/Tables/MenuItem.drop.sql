IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MenuItem_ParentId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MenuItem] DROP CONSTRAINT [DF_MenuItem_ParentId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MenuItem_PageId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MenuItem] DROP CONSTRAINT [DF_MenuItem_PageId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MenuItem_Type]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MenuItem] DROP CONSTRAINT [DF_MenuItem_Type]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MenuItem_CheckPermission]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MenuItem] DROP CONSTRAINT [DF_MenuItem_CheckPermission]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuItem]') AND type in (N'U'))
DROP TABLE [dbo].[MenuItem]
GO
