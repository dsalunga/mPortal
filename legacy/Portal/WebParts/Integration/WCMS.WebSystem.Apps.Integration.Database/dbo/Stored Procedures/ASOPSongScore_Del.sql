CREATE PROCEDURE [dbo].[MCSongScore_Del]
	@Id int
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM MCSongScore
		WHERE Id=@Id;

RETURN 0