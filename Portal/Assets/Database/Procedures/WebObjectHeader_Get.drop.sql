
if exists (select * from dbo.sysobjects where id = object_id(N'[WebObjectHeader_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebObjectHeader_Get]
GO


