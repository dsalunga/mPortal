
if exists (select * from dbo.sysobjects where id = object_id(N'[Download_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [Download_Set]
GO


