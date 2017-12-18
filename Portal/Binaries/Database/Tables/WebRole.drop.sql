IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebRoles_IsSystem]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebRole] DROP CONSTRAINT [DF_WebRoles_IsSystem]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebRoles_ParentId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebRole] DROP CONSTRAINT [DF_WebRoles_ParentId]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebRole]') AND type in (N'U'))
DROP TABLE [dbo].[WebRole]
GO
