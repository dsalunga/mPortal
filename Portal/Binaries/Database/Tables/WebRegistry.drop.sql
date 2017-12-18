IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebRegistry_StageId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebRegistry] DROP CONSTRAINT [DF_WebRegistry_StageId]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebRegistry]') AND type in (N'U'))
DROP TABLE [dbo].[WebRegistry]
GO
