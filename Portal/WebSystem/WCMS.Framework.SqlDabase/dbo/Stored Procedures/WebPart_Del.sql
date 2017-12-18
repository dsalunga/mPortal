CREATE PROCEDURE [dbo].[WebPart_Del]
	(
		@PartId int
	)
AS
	SET NOCOUNT ON
	
	IF(@PartId > 0)
		DELETE FROM WebPart
		WHERE PartId=@PartId
	
	RETURN