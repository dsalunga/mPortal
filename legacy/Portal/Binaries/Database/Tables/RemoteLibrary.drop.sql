IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RemoteLibrary]') AND type in (N'U'))
DROP TABLE [dbo].[RemoteLibrary]
GO
