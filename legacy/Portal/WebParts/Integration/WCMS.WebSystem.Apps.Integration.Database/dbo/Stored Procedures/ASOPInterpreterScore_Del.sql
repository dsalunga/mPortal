CREATE PROCEDURE [dbo].[MCInterpreterScore_Del]
	@Id int
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM MCInterpreterScore
		WHERE Id=@Id;

RETURN 0