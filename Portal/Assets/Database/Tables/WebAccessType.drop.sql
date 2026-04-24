IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebAccessType]') AND type in (N'U'))
DROP TABLE [dbo].[WebAccessType]
GO
