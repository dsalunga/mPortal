
if exists (select * from dbo.sysobjects where id = object_id(N'[WebShare_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebShare_Get]
GO


