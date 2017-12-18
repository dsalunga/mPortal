CREATE PROCEDURE [dbo].[WebTemplate_Del]
	(
		@Id int
	)
AS
	SET NOCOUNT ON
	
	if(@Id > 0)
		DELETE FROM WebTemplate
		WHERE Id=@Id

	RETURN