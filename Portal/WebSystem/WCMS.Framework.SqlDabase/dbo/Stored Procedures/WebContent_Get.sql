CREATE PROCEDURE [dbo].[WebContent_Get]
	(
		@ContentId int = -1,
		@VersionNo int = -1, -- if -2 then return history, 1 = working version (non-history)
		@VersionOf int = -2,
		@DirectoryId int = -2, -- -1 = Uncategorized
		@SiteId int = -2,
		@Title nvarchar(500) = NULL
	)
AS
	SET NOCOUNT ON
	
	SELECT        ContentId, Title, [Content], VersionOf, VersionNo, DirectoryId, Active, DateModified,
				SiteId, EditorSensitive, ActiveContent
	FROM            WebContent
	WHERE    
			 (@ContentId = -1 OR ContentId = @ContentId)
		 AND (@Title IS NULL OR Title=@Title)

	     AND (@VersionNo = -1 
					OR (@VersionNo = -2 AND VersionNo > 0) -- Get History
					OR VersionNo = @VersionNo)
	     AND (@VersionOf = -2 OR VersionOf = @VersionOf)
	     AND (@DirectoryId = -2 OR DirectoryId = @DirectoryId)
		 AND (@SiteId = -2 OR SiteId=@SiteId)   

	RETURN