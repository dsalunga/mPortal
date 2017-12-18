
if exists (select * from dbo.sysobjects where id = object_id(N'[DownloadProperty_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [DownloadProperty_Set]
GO


