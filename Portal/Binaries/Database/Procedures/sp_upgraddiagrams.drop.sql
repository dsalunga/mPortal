
if exists (select * from dbo.sysobjects where id = object_id(N'[sp_upgraddiagrams]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [sp_upgraddiagrams]
GO


