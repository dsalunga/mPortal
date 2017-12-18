IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MusicEntr__MusicId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MusicEntry] DROP CONSTRAINT [DF__MusicEntr__MusicId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MusicEntr__EntryTypeId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MusicEntry] DROP CONSTRAINT [DF__MusicEntr__EntryTypeId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MusicEntr__FileName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MusicEntry] DROP CONSTRAINT [DF__MusicEntr__FileName]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MusicEntry__Tags]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MusicEntry] DROP CONSTRAINT [DF__MusicEntry__Tags]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MusicEntr__DateModified]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MusicEntry] DROP CONSTRAINT [DF__MusicEntr__DateModified]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MusicEntr__FileSize]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MusicEntry] DROP CONSTRAINT [DF__MusicEntr__FileSize]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MusicEntry]') AND type in (N'U'))
DROP TABLE [dbo].[MusicEntry]
GO
