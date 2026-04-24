
if exists (select * from dbo.sysobjects where id = object_id(N'[MusicCompetition_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [MusicCompetition_Del]
GO


