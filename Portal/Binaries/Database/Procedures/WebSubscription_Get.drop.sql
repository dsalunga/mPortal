
if exists (select * from dbo.sysobjects where id = object_id(N'[WebSubscription_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebSubscription_Get]
GO


