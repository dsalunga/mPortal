SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LessonReviewerSession]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[LessonReviewerSession](
	[Id] [int] NOT NULL,
	[ServiceScheduleID] [int] NOT NULL,
	[ServiceStartDate] [datetime] NOT NULL,
	[ServiceName] [nvarchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DateStarted] [datetime] NOT NULL,
	[DateCompleted] [datetime] NOT NULL,
	[MemberId] [int] NOT NULL,
	[AbsentReason] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[WorkerNotes] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[WorkerUserId] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[DateApproved] [datetime] NOT NULL,
	[AdditionalNotes] [nvarchar](max) COLLATE Latin1_General_CI_AI NOT NULL,
	[AttendanceType] [int] NOT NULL,
	[PageId] [int] NOT NULL,
	[Extra] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_LessonReviewerSession] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LessonReviewerSession_ServiceScheduleId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LessonReviewerSession] ADD  CONSTRAINT [DF_LessonReviewerSession_ServiceScheduleId]  DEFAULT ((-1)) FOR [ServiceScheduleID]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LessonReviewerSession_ServiceStartDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LessonReviewerSession] ADD  CONSTRAINT [DF_LessonReviewerSession_ServiceStartDate]  DEFAULT (getdate()) FOR [ServiceStartDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LessonReviewerSession_ServiceName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LessonReviewerSession] ADD  CONSTRAINT [DF_LessonReviewerSession_ServiceName]  DEFAULT ('') FOR [ServiceName]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LessonReviewerSession_DateStarted]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LessonReviewerSession] ADD  CONSTRAINT [DF_LessonReviewerSession_DateStarted]  DEFAULT (getdate()) FOR [DateStarted]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LessonReviewerSession_DateCompleted]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LessonReviewerSession] ADD  CONSTRAINT [DF_LessonReviewerSession_DateCompleted]  DEFAULT (getdate()) FOR [DateCompleted]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LessonReviewerSession_MemberId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LessonReviewerSession] ADD  CONSTRAINT [DF_LessonReviewerSession_MemberId]  DEFAULT ((-1)) FOR [MemberId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LessonReviewerSession_AbsentReason]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LessonReviewerSession] ADD  CONSTRAINT [DF_LessonReviewerSession_AbsentReason]  DEFAULT ('') FOR [AbsentReason]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LessonReviewerSession_RejectReason]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LessonReviewerSession] ADD  CONSTRAINT [DF_LessonReviewerSession_RejectReason]  DEFAULT ('') FOR [WorkerNotes]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table1_AssignedWorkerId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LessonReviewerSession] ADD  CONSTRAINT [DF_Table1_AssignedWorkerId]  DEFAULT ((-1)) FOR [WorkerUserId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LessonReviewerSession_Status]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LessonReviewerSession] ADD  CONSTRAINT [DF_LessonReviewerSession_Status]  DEFAULT ((0)) FOR [Status]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LessonReviewerSession_DateApproved]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LessonReviewerSession] ADD  CONSTRAINT [DF_LessonReviewerSession_DateApproved]  DEFAULT (getdate()) FOR [DateApproved]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LessonReviewerSession_AdditionalNotes]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LessonReviewerSession] ADD  CONSTRAINT [DF_LessonReviewerSession_AdditionalNotes]  DEFAULT ('') FOR [AdditionalNotes]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__ServiceMa__AttendanceType]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LessonReviewerSession] ADD  CONSTRAINT [DF__ServiceMa__AttendanceType]  DEFAULT ((1)) FOR [AttendanceType]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__ServiceMa__PageId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LessonReviewerSession] ADD  CONSTRAINT [DF__ServiceMa__PageId]  DEFAULT ((-1)) FOR [PageId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__ServiceMa__Extra__53F07822]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LessonReviewerSession] ADD  CONSTRAINT [DF__ServiceMa__Extra__53F07822]  DEFAULT ('') FOR [Extra]
END

GO
