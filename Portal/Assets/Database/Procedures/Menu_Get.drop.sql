
if exists (select * from dbo.sysobjects where id = object_id(N'[Menu_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [Menu_Get]
GO


