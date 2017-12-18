IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPermission_IsSystem]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPermission] DROP CONSTRAINT [DF_WebPermission_IsSystem]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebPermission]') AND type in (N'U'))
DROP TABLE [dbo].[WebPermission]
GO
