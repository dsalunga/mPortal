CREATE PROCEDURE dbo.BibleVersionLanguage_Get
	(
		@Id int = -1
	)
AS
	SET NOCOUNT ON

	SELECT        Id, Name
	FROM            BibleVersionLanguage
	WHERE (@Id =-1 OR Id=@Id)

	RETURN