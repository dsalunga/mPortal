IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebParameter_IsRequired]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebParameter] DROP CONSTRAINT [DF_WebParameter_IsRequired]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebParameter]') AND type in (N'U'))
DROP TABLE [dbo].[WebParameter]
GO
