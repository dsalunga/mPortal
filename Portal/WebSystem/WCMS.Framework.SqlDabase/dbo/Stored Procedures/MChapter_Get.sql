CREATE PROCEDURE dbo.MChapter_Get
	(
		@ParentId int = -2,
		@Id int = -1
	)
AS
	SET NOCOUNT ON
	
	SELECT        Id, Name, ParentId, [Address], Mobile, Telephone, Email, ChapterType, Latitude, Longitude, 
		AccessType, CountryCode, ServiceSchedule, MoreInfo
	FROM            MChapter
	WHERE	(@ParentId = -2 OR ParentId=@ParentId) AND
			(@Id = -1 OR Id=@Id)
	
	RETURN