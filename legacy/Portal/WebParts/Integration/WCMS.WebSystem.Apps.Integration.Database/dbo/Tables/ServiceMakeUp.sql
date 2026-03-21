CREATE TABLE [dbo].[LessonReviewerSession] (
    [Id]                INT             NOT NULL,
    [ServiceScheduleID] INT             CONSTRAINT [DF_LessonReviewerSession_ServiceScheduleId] DEFAULT ((-1)) NOT NULL,
    [ServiceStartDate]  DATETIME        CONSTRAINT [DF_LessonReviewerSession_ServiceStartDate] DEFAULT (getdate()) NOT NULL,
    [ServiceName]       NVARCHAR (150)  CONSTRAINT [DF_LessonReviewerSession_ServiceName] DEFAULT ('') NOT NULL,
    [DateStarted]       DATETIME        CONSTRAINT [DF_LessonReviewerSession_DateStarted] DEFAULT (getdate()) NOT NULL,
    [DateCompleted]     DATETIME        CONSTRAINT [DF_LessonReviewerSession_DateCompleted] DEFAULT (getdate()) NOT NULL,
    [MemberId]          INT             CONSTRAINT [DF_LessonReviewerSession_MemberId] DEFAULT ((-1)) NOT NULL,
    [AbsentReason]      NVARCHAR (4000) CONSTRAINT [DF_LessonReviewerSession_AbsentReason] DEFAULT ('') NOT NULL,
    [CouncillorNotes]       NVARCHAR (4000) CONSTRAINT [DF_LessonReviewerSession_RejectReason] DEFAULT ('') NOT NULL,
    [CouncillorUserId]      INT             CONSTRAINT [DF_Table1_AssignedCouncillorId] DEFAULT ((-1)) NOT NULL,
    [Status]            INT             CONSTRAINT [DF_LessonReviewerSession_Status] DEFAULT ((0)) NOT NULL,
    [DateApproved]      DATETIME        CONSTRAINT [DF_LessonReviewerSession_DateApproved] DEFAULT (getdate()) NOT NULL,
    [AdditionalNotes]   NVARCHAR (4000) COLLATE Latin1_General_CI_AI CONSTRAINT [DF_LessonReviewerSession_AdditionalNotes] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_LessonReviewerSession] PRIMARY KEY CLUSTERED ([Id] ASC)
);

