IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebObjectContent]') AND type in (N'U'))
DROP TABLE [dbo].[WebObjectContent]
GO
