
if exists (select * from dbo.sysobjects where id = object_id(N'[Sportsfest_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [Sportsfest_Del]
GO


