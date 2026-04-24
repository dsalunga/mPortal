
if exists (select * from dbo.sysobjects where id = object_id(N'[ContactInquiry_Set]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [ContactInquiry_Set]
GO


