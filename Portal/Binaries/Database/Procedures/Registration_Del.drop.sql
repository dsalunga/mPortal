
if exists (select * from dbo.sysobjects where id = object_id(N'[Registration_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [Registration_Del]
GO


