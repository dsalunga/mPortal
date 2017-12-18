IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RemoteDataMap]') AND type in (N'U'))
DROP TABLE [dbo].[RemoteDataMap]
GO
