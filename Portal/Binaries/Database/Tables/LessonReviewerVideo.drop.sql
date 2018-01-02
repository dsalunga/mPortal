IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LessonReviewerVideo_ServiceScheduleId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LessonReviewerVideo] DROP CONSTRAINT [DF_LessonReviewerVideo_ServiceScheduleId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_LessonReviewerVideo_Duration]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[LessonReviewerVideo] DROP CONSTRAINT [DF_LessonReviewerVideo_Duration]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LessonReviewerVideo]') AND type in (N'U'))
DROP TABLE [dbo].[LessonReviewerVideo]
GO
