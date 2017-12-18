IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebCategor__Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebCategory] DROP CONSTRAINT [DF__WebCategor__Name]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebCatego__ObjectId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebCategory] DROP CONSTRAINT [DF__WebCatego__ObjectId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebCategor__Rank]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebCategory] DROP CONSTRAINT [DF__WebCategor__Rank]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebCatego__ParentId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebCategory] DROP CONSTRAINT [DF__WebCatego__ParentId]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebCategory]') AND type in (N'U'))
DROP TABLE [dbo].[WebCategory]
GO
