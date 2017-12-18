IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObjectSecurity_Public]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObjectSecurity] DROP CONSTRAINT [DF_WebObjectSecurity_Public]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebObjectSecurity]') AND type in (N'U'))
DROP TABLE [dbo].[WebObjectSecurity]
GO
