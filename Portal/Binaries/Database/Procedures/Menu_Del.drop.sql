
if exists (select * from dbo.sysobjects where id = object_id(N'[Menu_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [Menu_Del]
GO


