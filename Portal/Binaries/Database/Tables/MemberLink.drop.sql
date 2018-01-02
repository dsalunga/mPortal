IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_MemberId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] DROP CONSTRAINT [DF_MemberLink_MemberId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_ExternalIdNo]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] DROP CONSTRAINT [DF_MemberLink_ExternalIdNo]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_HomeAddressLine1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] DROP CONSTRAINT [DF_MemberLink_HomeAddressLine1]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_HomeAddressStateCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] DROP CONSTRAINT [DF_MemberLink_HomeAddressStateCode]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_HomeAddressCountryCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] DROP CONSTRAINT [DF_MemberLink_HomeAddressCountryCode]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_HomeAddressZipCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] DROP CONSTRAINT [DF_MemberLink_HomeAddressZipCode]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_MobileNumber]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] DROP CONSTRAINT [DF_MemberLink_MobileNumber]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_HomePhone]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] DROP CONSTRAINT [DF_MemberLink_HomePhone]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_WorkAddressLine1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] DROP CONSTRAINT [DF_MemberLink_WorkAddressLine1]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_WorkAddressStateCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] DROP CONSTRAINT [DF_MemberLink_WorkAddressStateCode]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_WorkAddressCountryCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] DROP CONSTRAINT [DF_MemberLink_WorkAddressCountryCode]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_WorkAddressZipCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] DROP CONSTRAINT [DF_MemberLink_WorkAddressZipCode]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_WorkDesignation]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] DROP CONSTRAINT [DF_MemberLink_WorkDesignation]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_WorkPhone]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] DROP CONSTRAINT [DF_MemberLink_WorkPhone]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_Nickname]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] DROP CONSTRAINT [DF_MemberLink_Nickname]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_LastUpdate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] DROP CONSTRAINT [DF_MemberLink_LastUpdate]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_PhotoPath]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] DROP CONSTRAINT [DF_MemberLink_PhotoPath]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_MembershipDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] DROP CONSTRAINT [DF_MemberLink_MembershipDate]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_Approved]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] DROP CONSTRAINT [DF_MemberLink_Approved]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_Locale]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] DROP CONSTRAINT [DF_MemberLink_Locale]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_HomeAddressLine11]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] DROP CONSTRAINT [DF_MemberLink_HomeAddressLine11]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_HomeAddressLine11_1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] DROP CONSTRAINT [DF_MemberLink_HomeAddressLine11_1]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_Private]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] DROP CONSTRAINT [DF_MemberLink_Private]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_AdditionalInformation]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] DROP CONSTRAINT [DF_MemberLink_AdditionalInformation]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MemberLin__Local__66A5BBE8]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] DROP CONSTRAINT [DF__MemberLin__Local__66A5BBE8]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MemberLink]') AND type in (N'U'))
DROP TABLE [dbo].[MemberLink]
GO
