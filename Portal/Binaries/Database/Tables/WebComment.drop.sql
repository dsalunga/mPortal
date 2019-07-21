IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebComment]') AND type in (N'U'))
DROP TABLE [dbo].[WebComment]
GO
