IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_AuditLog_LogDateTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventLog] DROP CONSTRAINT [DF_AuditLog_LogDateTime]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_AuditLog_Content]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventLog] DROP CONSTRAINT [DF_AuditLog_Content]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_EventLog_UserId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventLog] DROP CONSTRAINT [DF_EventLog_UserId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_EventLog_Action]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventLog] DROP CONSTRAINT [DF_EventLog_Action]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventLog]') AND type in (N'U'))
DROP TABLE [dbo].[EventLog]
GO
