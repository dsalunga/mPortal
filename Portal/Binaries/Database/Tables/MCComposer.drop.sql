IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCComposer__Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCComposer] DROP CONSTRAINT [DF__MCComposer__Name]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCComposer__Entry]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCComposer] DROP CONSTRAINT [DF__MCComposer__Entry]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCComposer__Locale]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCComposer] DROP CONSTRAINT [DF__MCComposer__Locale]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCComposer__Work]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCComposer] DROP CONSTRAINT [DF__MCComposer__Work]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCComposer__Description]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCComposer] DROP CONSTRAINT [DF__MCComposer__Description]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCComposer__PhotoFile]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCComposer] DROP CONSTRAINT [DF__MCComposer__PhotoFile]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCComposer__NickName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCComposer] DROP CONSTRAINT [DF__MCComposer__NickName]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCComposer__Active]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCComposer] DROP CONSTRAINT [DF__MCComposer__Active]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCComposer__CompetitionId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCComposer] DROP CONSTRAINT [DF__MCComposer__CompetitionId]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MCComposer]') AND type in (N'U'))
DROP TABLE [dbo].[MCComposer]
GO
