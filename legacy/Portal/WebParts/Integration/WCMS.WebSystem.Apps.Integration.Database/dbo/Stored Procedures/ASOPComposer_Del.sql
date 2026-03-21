CREATE PROCEDURE [dbo].[MCComposer_Del]
	@Id int
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM MCComposer
		WHERE Id=@Id;

RETURN 0