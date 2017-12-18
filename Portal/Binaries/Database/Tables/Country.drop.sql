IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Country_CountryName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Country] DROP CONSTRAINT [DF_Country_CountryName]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Country_RegionCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Country] DROP CONSTRAINT [DF_Country_RegionCode]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Country_Description]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Country] DROP CONSTRAINT [DF_Country_Description]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Country_ISOCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Country] DROP CONSTRAINT [DF_Country_ISOCode]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Country_DialingCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Country] DROP CONSTRAINT [DF_Country_DialingCode]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Country_MaxPhoneDigit]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Country] DROP CONSTRAINT [DF_Country_MaxPhoneDigit]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Country__ISOCode3]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Country] DROP CONSTRAINT [DF__Country__ISOCode3]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Country__ShortName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Country] DROP CONSTRAINT [DF__Country__ShortName]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Country__ISONumeric]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Country] DROP CONSTRAINT [DF__Country__ISONumeric]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Country]') AND type in (N'U'))
DROP TABLE [dbo].[Country]
GO
