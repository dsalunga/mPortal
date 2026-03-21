CREATE TABLE [dbo].[LessonReviewerVideo] (
    [ServiceStartDate]  DATETIME NOT NULL,
    [ServiceScheduleId] INT      CONSTRAINT [DF_LessonReviewerVideo_ServiceScheduleId] DEFAULT ((-1)) NOT NULL,
    [Duration]          INT      CONSTRAINT [DF_LessonReviewerVideo_Duration] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_LessonReviewerVideo] PRIMARY KEY CLUSTERED ([ServiceScheduleId] ASC)
);

