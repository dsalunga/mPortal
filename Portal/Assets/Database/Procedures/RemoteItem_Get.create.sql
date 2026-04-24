
-- Procedure RemoteItem_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.RemoteItem_Get
	(
		@Id int = -1,
		@LibraryId int = -2,
		@ParentId int = -2,
		@Cached int = -1
	)
AS
	SET NOCOUNT ON

	SELECT        Id, LibraryId, Name, RelativePath, TypeId, DateModified, Size, [Content], ParentId, DownloadCount,
		DisplayName, IndexDateModified, FileCacheEnabled, Cached
	FROM            RemoteItem
	WHERE        
		(@Id = -1 OR Id = @Id) AND 
		(@LibraryId = -2 OR LibraryId = @LibraryId) AND 
		(@ParentId = -2 OR ParentId = @ParentId) AND
		(@Cached = -1 OR Cached=@Cached)
	ORDER BY Name

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

