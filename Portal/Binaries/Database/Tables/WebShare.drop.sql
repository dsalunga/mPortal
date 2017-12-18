IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebShare__Object]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebShare] DROP CONSTRAINT [DF__WebShare__Object]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebShare__Record]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebShare] DROP CONSTRAINT [DF__WebShare__Record]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebShare__ShareO]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebShare] DROP CONSTRAINT [DF__WebShare__ShareO]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebShare__ShareR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebShare] DROP CONSTRAINT [DF__WebShare__ShareR]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebShare__Allow]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebShare] DROP CONSTRAINT [DF__WebShare__Allow]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebShare]') AND type in (N'U'))
DROP TABLE [dbo].[WebShare]
GO
