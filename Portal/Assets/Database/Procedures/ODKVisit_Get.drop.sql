
if exists (select * from dbo.sysobjects where id = object_id(N'[ODKVisit_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [ODKVisit_Get]
GO


