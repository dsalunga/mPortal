
if exists (select * from dbo.sysobjects where id = object_id(N'[GenericList_GetLinkAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [GenericList_GetLinkAll]
GO


