CREATE PROCEDURE [dbo].[WebPartControlTemplate_Del]
	(
		@PartControlTemplateId int
	)
AS
	SET NOCOUNT ON
	
	IF(@PartControlTemplateId > 0)
		DELETE FROM WebPartControlTemplate
		WHERE PartControlTemplateId=@PartControlTemplateId
	
	RETURN