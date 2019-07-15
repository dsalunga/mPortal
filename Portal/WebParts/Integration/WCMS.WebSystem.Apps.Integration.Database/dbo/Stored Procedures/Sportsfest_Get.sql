CREATE PROCEDURE dbo.Sportsfest_Get
	(
		@Id int = -1,
		@GroupColor nvarchar(50) = NULL,
		@Name nvarchar(100) = NULL
	)
AS
	SET NOCOUNT ON

	SELECT        Id, MemberId, Name, GroupColor, Age, Mobile, EntryDate, Sports, Locale, Suggestion, CountryCode,
			ShirtSize
	FROM            Sportsfest
	WHERE        (@Id = -1 OR Id = @Id) 
		AND (@GroupColor IS NULL OR GroupColor = @GroupColor)
		AND (@Name IS NULL OR Name=@Name)

	RETURN