
if exists (select * from dbo.sysobjects where id = object_id(N'[ContactInquiry_XML]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [ContactInquiry_XML]
GO


