IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPermissionSet_RecordId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPermissionSet] DROP CONSTRAINT [DF_WebPermissionSet_RecordId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPermissionSet_Public]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPermissionSet] DROP CONSTRAINT [DF_WebPermissionSet_Public]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebPermissionSet]') AND type in (N'U'))
DROP TABLE [dbo].[WebPermissionSet]
GO
