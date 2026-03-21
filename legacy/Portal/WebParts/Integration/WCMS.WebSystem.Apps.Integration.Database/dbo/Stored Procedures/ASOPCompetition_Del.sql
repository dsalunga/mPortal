CREATE PROCEDURE [dbo].[MusicCompetition_Del]
	@Id int
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM MusicCompetition
		WHERE Id=@Id;

RETURN 0