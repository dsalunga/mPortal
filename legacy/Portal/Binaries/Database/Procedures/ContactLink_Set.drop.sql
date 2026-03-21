
if exists (select * from dbo.sysobjects where id = object_id(N'[ContactLink_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [ContactLink_Set]
GO


