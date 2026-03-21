
if exists (select * from dbo.sysobjects where id = object_id(N'[WebSite_GetMaxRank]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebSite_GetMaxRank]
GO


