
if exists (select * from dbo.sysobjects where id = object_id(N'[RemoteItem_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [RemoteItem_Del]
GO


