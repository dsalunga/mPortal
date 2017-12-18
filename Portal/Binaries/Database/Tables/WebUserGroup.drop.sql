IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUserGroup_Active]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUserGroup] DROP CONSTRAINT [DF_WebUserGroup_Active]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUserGroup_DateJoined]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUserGroup] DROP CONSTRAINT [DF_WebUserGroup_DateJoined]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUserGroup_ObjectId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUserGroup] DROP CONSTRAINT [DF_WebUserGroup_ObjectId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUserGroup_RecordId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUserGroup] DROP CONSTRAINT [DF_WebUserGroup_RecordId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUserGroup_Remarks]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUserGroup] DROP CONSTRAINT [DF_WebUserGroup_Remarks]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUserGroup_CreatedById]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUserGroup] DROP CONSTRAINT [DF_WebUserGroup_CreatedById]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebUserGroup]') AND type in (N'U'))
DROP TABLE [dbo].[WebUserGroup]
GO
