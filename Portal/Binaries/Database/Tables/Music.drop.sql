IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Music__Title]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Music] DROP CONSTRAINT [DF__Music__Title]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Music__LanguageCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Music] DROP CONSTRAINT [DF__Music__LanguageCode]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Music__CountryCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Music] DROP CONSTRAINT [DF__Music__CountryCode]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Music__FolderName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Music] DROP CONSTRAINT [DF__Music__FolderName]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Music__Tags]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Music] DROP CONSTRAINT [DF__Music__Tags]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Music__IsOriginal]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Music] DROP CONSTRAINT [DF__Music__IsOriginal]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Music__ParentId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Music] DROP CONSTRAINT [DF__Music__ParentId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Music__CategoryId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Music] DROP CONSTRAINT [DF__Music__CategoryId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Music__DateModified]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Music] DROP CONSTRAINT [DF__Music__DateModified]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Music__Composer]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Music] DROP CONSTRAINT [DF__Music__Composer]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Music__DateComposed]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Music] DROP CONSTRAINT [DF__Music__DateComposed]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Music]') AND type in (N'U'))
DROP TABLE [dbo].[Music]
GO
