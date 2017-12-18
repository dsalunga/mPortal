IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebApplication_AppKey]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebApplication] DROP CONSTRAINT [DF_WebApplication_AppKey]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebApplication_IpAddresses]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebApplication] DROP CONSTRAINT [DF_WebApplication_IpAddresses]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebApplication]') AND type in (N'U'))
DROP TABLE [dbo].[WebApplication]
GO
