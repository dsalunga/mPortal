IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MenuObject_ParameterSetId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MenuObject] DROP CONSTRAINT [DF_MenuObject_ParameterSetId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MenuObject_RenderMode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MenuObject] DROP CONSTRAINT [DF_MenuObject_RenderMode]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuObject]') AND type in (N'U'))
DROP TABLE [dbo].[MenuObject]
GO
