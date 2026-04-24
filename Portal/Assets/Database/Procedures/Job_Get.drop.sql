
if exists (select * from dbo.sysobjects where id = object_id(N'[Job_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [Job_Get]
GO


