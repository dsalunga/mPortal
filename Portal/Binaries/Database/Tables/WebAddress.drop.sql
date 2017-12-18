IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table1_HomeAddressLine1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebAddress] DROP CONSTRAINT [DF_Table1_HomeAddressLine1]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebAddress_AddressLine2]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebAddress] DROP CONSTRAINT [DF_WebAddress_AddressLine2]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebAddress_CityTown]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebAddress] DROP CONSTRAINT [DF_WebAddress_CityTown]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebAddress_StateProvince]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebAddress] DROP CONSTRAINT [DF_WebAddress_StateProvince]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table1_HomeAddressStateCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebAddress] DROP CONSTRAINT [DF_Table1_HomeAddressStateCode]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table1_HomeAddressCountryCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebAddress] DROP CONSTRAINT [DF_Table1_HomeAddressCountryCode]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table1_HomeAddressZipCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebAddress] DROP CONSTRAINT [DF_Table1_HomeAddressZipCode]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebAddress_PhoneNumber]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebAddress] DROP CONSTRAINT [DF_WebAddress_PhoneNumber]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebAddress_UserId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebAddress] DROP CONSTRAINT [DF_WebAddress_UserId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebAddress_RecordId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebAddress] DROP CONSTRAINT [DF_WebAddress_RecordId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebAddress_LastUpdated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebAddress] DROP CONSTRAINT [DF_WebAddress_LastUpdated]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebAddress]') AND type in (N'U'))
DROP TABLE [dbo].[WebAddress]
GO
