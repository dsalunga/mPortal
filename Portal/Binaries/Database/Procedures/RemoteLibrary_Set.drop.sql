
if exists (select * from dbo.sysobjects where id = object_id(N'[RemoteLibrary_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [RemoteLibrary_Set]
GO


