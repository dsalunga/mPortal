SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LessonReviewerVideo]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[LessonReviewerVideo](
	[ServiceStartDate] [datetime] NOT NULL,
	[ServiceScheduleId] [int] NOT NULL,
	[Duration] [int] NOT NULL,
 CONSTRAINT [PK_LessonReviewerVideo] PRIMARY KEY CLUSTERED 
(
	[ServiceScheduleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LessonReviewerVideo_ServiceScheduleId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LessonReviewerVideo] ADD  CONSTRAINT [DF_LessonReviewerVideo_ServiceScheduleId]  DEFAULT ((-1)) FOR [ServiceScheduleId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LessonReviewerVideo_Duration]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LessonReviewerVideo] ADD  CONSTRAINT [DF_LessonReviewerVideo_Duration]  DEFAULT ((-1)) FOR [Duration]
END

GO
