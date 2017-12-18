IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_USState_StateName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CountryState] DROP CONSTRAINT [DF_USState_StateName]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_USState_ZipCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CountryState] DROP CONSTRAINT [DF_USState_ZipCode]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_USState_CountryCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CountryState] DROP CONSTRAINT [DF_USState_CountryCode]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CountryState]') AND type in (N'U'))
DROP TABLE [dbo].[CountryState]
GO
