IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebPermissionSet]') AND type in (N'U'))
DROP TABLE [dbo].[WebPermissionSet]
GO
