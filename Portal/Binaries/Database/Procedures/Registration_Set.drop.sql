
if exists (select * from dbo.sysobjects where id = object_id(N'[Registration_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [Registration_Set]
GO


