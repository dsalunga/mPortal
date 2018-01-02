CREATE PROCEDURE [dbo].[BibleVersion_Get]
	(
		@Id int = -1,
		@LanguageType int =-2,
		@TranslationType int = -2
	)
AS
	SET NOCOUNT ON

	SELECT        BibleTableName, Name, BookNameCode, OldAndNew, Id, ShortName, Active, LanguageType, TranslationType
	FROM            BibleVersion
	WHERE (@Id =-1 OR Id=@Id)
		AND (@TranslationType = -2 OR TranslationType=@TranslationType)
		AND (@LanguageType=-2 OR LanguageType=@LanguageType)
		AND Active = 1;

	RETURN