IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebMessageQueue_FromObjectId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebMessageQueue] DROP CONSTRAINT [DF_WebMessageQueue_FromObjectId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebMessageQueue_FromRecordId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebMessageQueue] DROP CONSTRAINT [DF_WebMessageQueue_FromRecordId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebMessageQueue_ToOrBcc]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebMessageQueue] DROP CONSTRAINT [DF_WebMessageQueue_ToOrBcc]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebMessageQueue_DateCreated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebMessageQueue] DROP CONSTRAINT [DF_WebMessageQueue_DateCreated]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebMessageQueue_DateSent]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebMessageQueue] DROP CONSTRAINT [DF_WebMessageQueue_DateSent]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebMessageQueue_Status]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebMessageQueue] DROP CONSTRAINT [DF_WebMessageQueue_Status]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebMessageQueue_EnableMonitor]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebMessageQueue] DROP CONSTRAINT [DF_WebMessageQueue_EnableMonitor]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebMessageQueue]') AND type in (N'U'))
DROP TABLE [dbo].[WebMessageQueue]
GO
