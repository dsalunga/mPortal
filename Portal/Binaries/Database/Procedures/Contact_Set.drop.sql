
if exists (select * from dbo.sysobjects where id = object_id(N'[Contact_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [Contact_Set]
GO


