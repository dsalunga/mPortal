
if exists (select * from dbo.sysobjects where id = object_id(N'[WebObjects_NextId]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebObjects_NextId]
GO


