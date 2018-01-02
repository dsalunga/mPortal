
if exists (select * from dbo.sysobjects where id = object_id(N'[sp_helpdiagramdefinition]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [sp_helpdiagramdefinition]
GO


