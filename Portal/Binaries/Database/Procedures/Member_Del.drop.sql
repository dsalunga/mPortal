
if exists (select * from dbo.sysobjects where id = object_id(N'[Member_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [Member_Del]
GO


