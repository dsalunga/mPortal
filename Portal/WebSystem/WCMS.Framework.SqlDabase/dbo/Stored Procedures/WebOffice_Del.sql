CREATE PROCEDURE dbo.WebOffice_Del
	(
		@OfficeId int
	)
AS
	SET NOCOUNT ON
	
	IF(@OfficeId > 0)
		DELETE FROM WebOffice
		WHERE OfficeId= @OfficeId
	
	RETURN