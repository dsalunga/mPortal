
if exists (select * from dbo.sysobjects where id = object_id(N'[Newsletter_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [Newsletter_Get]
GO


