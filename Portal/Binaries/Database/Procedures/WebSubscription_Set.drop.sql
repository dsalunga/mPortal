
if exists (select * from dbo.sysobjects where id = object_id(N'[WebSubscription_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [WebSubscription_Set]
GO


