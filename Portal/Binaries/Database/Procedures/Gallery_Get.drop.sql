
if exists (select * from dbo.sysobjects where id = object_id(N'[Gallery_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [Gallery_Get]
GO


