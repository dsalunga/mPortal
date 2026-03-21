
if exists (select * from dbo.sysobjects where id = object_id(N'[SiteListProperty_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [SiteListProperty_Get]
GO


