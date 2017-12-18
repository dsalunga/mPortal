IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPartControl_AdminPartId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPartControl] DROP CONSTRAINT [DF_WebPartControl_AdminPartId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebPartCo__Entry]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPartControl] DROP CONSTRAINT [DF__WebPartCo__Entry]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPartControl_ParentId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPartControl] DROP CONSTRAINT [DF_WebPartControl_ParentId]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebPartControl]') AND type in (N'U'))
DROP TABLE [dbo].[WebPartControl]
GO
