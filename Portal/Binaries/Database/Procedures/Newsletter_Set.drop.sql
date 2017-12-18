
if exists (select * from dbo.sysobjects where id = object_id(N'[Newsletter_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [Newsletter_Set]
GO


