CREATE PROCEDURE [dbo].[WebPartControl_Del]
	(
		@PartControlId int
	)
AS
	SET NOCOUNT ON
	
	IF(@PartControlId > 0)
		DELETE FROM WebPartControl
		WHERE PartControlId=@PartControlId
	
	RETURN