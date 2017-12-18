IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebJob_Id]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebJob] DROP CONSTRAINT [DF_WebJob_Id]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebJob_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebJob] DROP CONSTRAINT [DF_WebJob_Name]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebJob_RecurrenceId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebJob] DROP CONSTRAINT [DF_WebJob_RecurrenceId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebJob_Weekdays]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebJob] DROP CONSTRAINT [DF_WebJob_Weekdays]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebJob_OccursEvery]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebJob] DROP CONSTRAINT [DF_WebJob_OccursEvery]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebJob_ExecutionStartDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebJob] DROP CONSTRAINT [DF_WebJob_ExecutionStartDate]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebJob_ExecutionEndDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebJob] DROP CONSTRAINT [DF_WebJob_ExecutionEndDate]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebJob_ExecutionStatus]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebJob] DROP CONSTRAINT [DF_WebJob_ExecutionStatus]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebJob_ExecutionMessage]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebJob] DROP CONSTRAINT [DF_WebJob_ExecutionMessage]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebJob_Enabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebJob] DROP CONSTRAINT [DF_WebJob_Enabled]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebJob_TypeName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebJob] DROP CONSTRAINT [DF_WebJob_TypeName]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebJob_StartDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebJob] DROP CONSTRAINT [DF_WebJob_StartDate]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebJob_Description]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebJob] DROP CONSTRAINT [DF_WebJob_Description]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebJob]') AND type in (N'U'))
DROP TABLE [dbo].[WebJob]
GO
