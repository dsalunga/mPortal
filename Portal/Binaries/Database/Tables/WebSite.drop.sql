IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebSite]') AND type in (N'U'))
DROP TABLE [dbo].[WebSite]
GO
