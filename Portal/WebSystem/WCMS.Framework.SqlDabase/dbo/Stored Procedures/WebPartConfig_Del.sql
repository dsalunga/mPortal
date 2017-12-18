CREATE PROCEDURE [dbo].[WebPartConfig_Del]
	(
		@PartConfigId int
	)
AS
	SET NOCOUNT ON
	
	IF(@PartConfigId > 0)
		DELETE FROM WebPartConfig
		WHERE PartConfigId=@PartConfigId
		
	RETURN