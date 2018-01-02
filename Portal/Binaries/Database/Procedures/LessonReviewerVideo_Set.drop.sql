
if exists (select * from dbo.sysobjects where id = object_id(N'[LessonReviewerVideo_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [LessonReviewerVideo_Set]
GO


