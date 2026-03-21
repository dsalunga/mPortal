IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Registration_EntryDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Registration] DROP CONSTRAINT [DF_Registration_EntryDate]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Registration_Country]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Registration] DROP CONSTRAINT [DF_Registration_Country]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Registration_Locale]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Registration] DROP CONSTRAINT [DF_Registration_Locale]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Registration_ExternalId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Registration] DROP CONSTRAINT [DF_Registration_ExternalId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Registration_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Registration] DROP CONSTRAINT [DF_Registration_Name]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Registration_Designation]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Registration] DROP CONSTRAINT [DF_Registration_Designation]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Registration_Airline]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Registration] DROP CONSTRAINT [DF_Registration_Airline]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Registration_FlightNo]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Registration] DROP CONSTRAINT [DF_Registration_FlightNo]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Registration_Suggestion]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Registration] DROP CONSTRAINT [DF_Registration_Suggestion]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Registration_Age]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Registration] DROP CONSTRAINT [DF_Registration_Age]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Registration_Gender]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Registration] DROP CONSTRAINT [DF_Registration_Gender]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Registration_PlaceType]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Registration] DROP CONSTRAINT [DF_Registration_PlaceType]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Registration]') AND type in (N'U'))
DROP TABLE [dbo].[Registration]
GO
