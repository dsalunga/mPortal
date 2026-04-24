
if exists (select * from dbo.sysobjects where id = object_id(N'[Sportsfest_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [Sportsfest_Get]
GO


