CREATE PROCEDURE [dbo].[WebPartAdmin_Del]
	(
		@PartAdminId int
	)
AS
	SET NOCOUNT ON
	
	if(@PartAdminId > 0)
		BEGIN
			DELETE FROM WebPartAdmin
			WHERE PartAdminId=@PartAdminId
		END
	
	RETURN