
if exists (select * from dbo.sysobjects where id = object_id(N'[WebObjectContent_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebObjectContent_Get]
GO


