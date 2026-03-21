
-- Procedure MChapter_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.MChapter_Get
	(
		@ParentId int = -2,
		@Id int = -1,
		@LocaleId int = -2,
		@Name nvarchar(500) = NULL
	)
AS
	SET NOCOUNT ON
	
	SELECT        Id, Name, ParentId, [Address], Mobile, Telephone, Email, ChapterType, Latitude, Longitude, 
		AccessType, CountryCode, ServiceSchedule, MoreInfo, DistrictNo, DivisionId, LastUpdate, LocaleId,
		Extra
	FROM            MChapter
	WHERE	(@ParentId = -2 OR ParentId=@ParentId)
			AND (@Id = -1 OR Id=@Id)
			AND (@LocaleId = -2 OR LocaleId=@LocaleId)
			AND (@Name IS NULL OR Name=@Name)

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

