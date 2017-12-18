
-- Procedure RemoteLibrary_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.RemoteLibrary_Get
	(
		@Id int = -1
	)
AS
	SET NOCOUNT ON

	SELECT        Id, Name, SourceTypeId, BaseAddress, UserName, Password, LastIndexDate, Active, DisplayBaseAddress,
		DownloadCountSince, FileCacheEnabled, FileCacheFolder, FileCacheMinDownloadCount, FileCacheCeilingSize, 
		FileCacheMaxSize, FileCacheMinDiskFreeMB, Size
	FROM            RemoteLibrary
	WHERE
		(@Id = -1 OR Id=@Id)

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

