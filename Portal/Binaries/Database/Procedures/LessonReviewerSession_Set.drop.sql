
if exists (select * from dbo.sysobjects where id = object_id(N'[LessonReviewerSession_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [LessonReviewerSession_Set]
GO


