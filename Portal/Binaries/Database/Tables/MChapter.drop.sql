IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MChapter_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] DROP CONSTRAINT [DF_MChapter_Name]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MChapter_ParentId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] DROP CONSTRAINT [DF_MChapter_ParentId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebOffice_Address]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] DROP CONSTRAINT [DF_WebOffice_Address]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MChapter_ChapterType]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] DROP CONSTRAINT [DF_MChapter_ChapterType]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MChapter_Latitude]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] DROP CONSTRAINT [DF_MChapter_Latitude]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MChapter_Longitude]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] DROP CONSTRAINT [DF_MChapter_Longitude]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MChapter_AccessType]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] DROP CONSTRAINT [DF_MChapter_AccessType]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MChapter_CountryCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] DROP CONSTRAINT [DF_MChapter_CountryCode]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MChapter_Email]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] DROP CONSTRAINT [DF_MChapter_Email]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MChapter_Mobile]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] DROP CONSTRAINT [DF_MChapter_Mobile]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MChapter_Telephone]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] DROP CONSTRAINT [DF_MChapter_Telephone]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MChapter_ServiceSchedule]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] DROP CONSTRAINT [DF_MChapter_ServiceSchedule]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MChapter_MoreInfo]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] DROP CONSTRAINT [DF_MChapter_MoreInfo]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MChapter_DistrictNo]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] DROP CONSTRAINT [DF_MChapter_DistrictNo]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MChapter_DivisionId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] DROP CONSTRAINT [DF_MChapter_DivisionId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MChapter_LastUpdate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] DROP CONSTRAINT [DF_MChapter_LastUpdate]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MChapter__Extra__3789439E]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] DROP CONSTRAINT [DF__MChapter__Extra__3789439E]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MChapter__Locale__387D67D7]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] DROP CONSTRAINT [DF__MChapter__Locale__387D67D7]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MChapter]') AND type in (N'U'))
DROP TABLE [dbo].[MChapter]
GO
