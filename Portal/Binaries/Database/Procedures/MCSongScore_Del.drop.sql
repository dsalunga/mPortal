
if exists (select * from dbo.sysobjects where id = object_id(N'[MCSongScore_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [MCSongScore_Del]
GO


