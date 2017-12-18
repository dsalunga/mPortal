
if exists (select * from dbo.sysobjects where id = object_id(N'[DownloadProperty_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [DownloadProperty_Get]
GO


