
if exists (select * from dbo.sysobjects where id = object_id(N'[DownloadLocation_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [DownloadLocation_Get]
GO


