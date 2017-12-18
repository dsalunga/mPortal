IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebGroup_ParentId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebGroup] DROP CONSTRAINT [DF_WebGroup_ParentId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebGroup_IsSystem]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebGroup] DROP CONSTRAINT [DF_WebGroup_IsSystem]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebGroup_DateModified]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebGroup] DROP CONSTRAINT [DF_WebGroup_DateModified]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebGroup_OwnerId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebGroup] DROP CONSTRAINT [DF_WebGroup_OwnerId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebGroup_JoinApproval]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebGroup] DROP CONSTRAINT [DF_WebGroup_JoinApproval]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebGroup_JoinAlert]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebGroup] DROP CONSTRAINT [DF_WebGroup_JoinAlert]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebGroup_PageUrl]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebGroup] DROP CONSTRAINT [DF_WebGroup_PageUrl]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebGroup_PageId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebGroup] DROP CONSTRAINT [DF_WebGroup_PageId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebGroup_Description]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebGroup] DROP CONSTRAINT [DF_WebGroup_Description]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebGroup__Managers]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebGroup] DROP CONSTRAINT [DF__WebGroup__Managers]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebGroup]') AND type in (N'U'))
DROP TABLE [dbo].[WebGroup]
GO
