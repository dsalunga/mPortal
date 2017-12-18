IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebConsta__Category]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebConstant] DROP CONSTRAINT [DF__WebConsta__Category]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebConsta__ObjectId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebConstant] DROP CONSTRAINT [DF__WebConsta__ObjectId]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebConstant]') AND type in (N'U'))
DROP TABLE [dbo].[WebConstant]
GO
