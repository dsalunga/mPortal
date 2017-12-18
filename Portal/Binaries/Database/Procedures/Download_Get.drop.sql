
if exists (select * from dbo.sysobjects where id = object_id(N'[Download_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [Download_Get]
GO


