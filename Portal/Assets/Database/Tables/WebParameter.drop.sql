IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebParameter]') AND type in (N'U'))
DROP TABLE [dbo].[WebParameter]
GO
