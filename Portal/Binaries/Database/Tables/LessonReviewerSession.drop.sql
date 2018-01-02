IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LessonReviewerSession_ServiceScheduleId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LessonReviewerSession] DROP CONSTRAINT [DF_LessonReviewerSession_ServiceScheduleId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LessonReviewerSession_ServiceStartDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LessonReviewerSession] DROP CONSTRAINT [DF_LessonReviewerSession_ServiceStartDate]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LessonReviewerSession_ServiceName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LessonReviewerSession] DROP CONSTRAINT [DF_LessonReviewerSession_ServiceName]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LessonReviewerSession_DateStarted]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LessonReviewerSession] DROP CONSTRAINT [DF_LessonReviewerSession_DateStarted]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LessonReviewerSession_DateCompleted]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LessonReviewerSession] DROP CONSTRAINT [DF_LessonReviewerSession_DateCompleted]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LessonReviewerSession_MemberId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LessonReviewerSession] DROP CONSTRAINT [DF_LessonReviewerSession_MemberId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LessonReviewerSession_AbsentReason]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LessonReviewerSession] DROP CONSTRAINT [DF_LessonReviewerSession_AbsentReason]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LessonReviewerSession_RejectReason]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LessonReviewerSession] DROP CONSTRAINT [DF_LessonReviewerSession_RejectReason]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table1_AssignedWorkerId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LessonReviewerSession] DROP CONSTRAINT [DF_Table1_AssignedWorkerId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LessonReviewerSession_Status]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LessonReviewerSession] DROP CONSTRAINT [DF_LessonReviewerSession_Status]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LessonReviewerSession_DateApproved]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LessonReviewerSession] DROP CONSTRAINT [DF_LessonReviewerSession_DateApproved]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LessonReviewerSession_AdditionalNotes]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LessonReviewerSession] DROP CONSTRAINT [DF_LessonReviewerSession_AdditionalNotes]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__ServiceMa__AttendanceType]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LessonReviewerSession] DROP CONSTRAINT [DF__ServiceMa__AttendanceType]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__ServiceMa__PageId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LessonReviewerSession] DROP CONSTRAINT [DF__ServiceMa__PageId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__ServiceMa__Extra__53F07822]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LessonReviewerSession] DROP CONSTRAINT [DF__ServiceMa__Extra__53F07822]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LessonReviewerSession]') AND type in (N'U'))
DROP TABLE [dbo].[LessonReviewerSession]
GO
