IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Wall_Content]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WallUpdate] DROP CONSTRAINT [DF_Wall_Content]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Wall_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WallUpdate] DROP CONSTRAINT [DF_Wall_UpdateDate]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Wall_EventTypeId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WallUpdate] DROP CONSTRAINT [DF_Wall_EventTypeId]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WallUpdate]') AND type in (N'U'))
DROP TABLE [dbo].[WallUpdate]
GO
