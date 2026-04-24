
if exists (select * from dbo.sysobjects where id = object_id(N'[SiteListProperty_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [SiteListProperty_Set]
GO


