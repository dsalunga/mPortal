IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Sportsfest_MemberId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Sportsfest] DROP CONSTRAINT [DF_Sportsfest_MemberId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Sportsfest_EntryDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Sportsfest] DROP CONSTRAINT [DF_Sportsfest_EntryDate]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Sportsfest_Locale]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Sportsfest] DROP CONSTRAINT [DF_Sportsfest_Locale]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Sportsfest_Suggestion]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Sportsfest] DROP CONSTRAINT [DF_Sportsfest_Suggestion]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Sportsfest_CountryCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Sportsfest] DROP CONSTRAINT [DF_Sportsfest_CountryCode]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Sportsfest_ShirtSize]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Sportsfest] DROP CONSTRAINT [DF_Sportsfest_ShirtSize]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sportsfest]') AND type in (N'U'))
DROP TABLE [dbo].[Sportsfest]
GO
