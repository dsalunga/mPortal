IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Membership]') AND type in (N'U'))
DROP TABLE [dbo].[Membership]
GO
