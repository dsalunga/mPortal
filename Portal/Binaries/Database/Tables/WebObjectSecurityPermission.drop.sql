IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObjectSecurityPermission_Allow]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObjectSecurityPermission] DROP CONSTRAINT [DF_WebObjectSecurityPermission_Allow]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObjectSecurityPermission_Deny]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObjectSecurityPermission] DROP CONSTRAINT [DF_WebObjectSecurityPermission_Deny]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebObjectSecurityPermission]') AND type in (N'U'))
DROP TABLE [dbo].[WebObjectSecurityPermission]
GO
