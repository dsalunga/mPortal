IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Download]') AND type in (N'U'))
DROP TABLE [dbo].[Download]
GO
