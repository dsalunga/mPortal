IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebParameterSet_Id]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebParameterSet] DROP CONSTRAINT [DF_WebParameterSet_Id]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebParameterSet_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebParameterSet] DROP CONSTRAINT [DF_WebParameterSet_Name]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebParameterSet_SchemaParameterName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebParameterSet] DROP CONSTRAINT [DF_WebParameterSet_SchemaParameterName]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebParameterSet]') AND type in (N'U'))
DROP TABLE [dbo].[WebParameterSet]
GO
