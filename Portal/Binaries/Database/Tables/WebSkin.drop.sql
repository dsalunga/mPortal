IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebSkin__Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSkin] DROP CONSTRAINT [DF__WebSkin__Name]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebSkin__Rank]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSkin] DROP CONSTRAINT [DF__WebSkin__Rank]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebSkin__ObjectId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSkin] DROP CONSTRAINT [DF__WebSkin__ObjectId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebSkin__RecordId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSkin] DROP CONSTRAINT [DF__WebSkin__RecordId]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebSkin]') AND type in (N'U'))
DROP TABLE [dbo].[WebSkin]
GO
