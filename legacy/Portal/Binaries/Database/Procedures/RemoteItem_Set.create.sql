
-- Procedure RemoteItem_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.RemoteItem_Set
	(
		@Id int = -1,
		@LibraryId int,
		@Name nvarchar(300),
		@RelativePath nvarchar(500),
		@TypeId int,
		@DateModified datetime,
		@IndexDateModified datetime,
		@Size bigint,
		@Content ntext,
		@ParentId int,
		@DownloadCount int,
		@DisplayName nvarchar(500),
		@FileCacheEnabled int,
		@Cached int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- UPDATE

			UPDATE       RemoteItem
			SET                LibraryId = @LibraryId, Name = @Name, RelativePath = @RelativePath, TypeId = @TypeId, DateModified = @DateModified, Size = @Size, [Content] = @Content, 
							ParentId = @ParentId, DownloadCount=@DownloadCount, DisplayName=@DisplayName, IndexDateModified=@IndexDateModified, FileCacheEnabled=@FileCacheEnabled,
							Cached=@Cached
			WHERE Id=@Id
		END
	ELSE
		BEGIN
			-- INSERT

			EXEC @Id = WebObject_NextId 'RemoteItem';

			INSERT INTO RemoteItem
			                         (LibraryId, Name, RelativePath, TypeId, DateModified, Size, [Content], ParentId, Id, DownloadCount,
						DisplayName, IndexDateModified, FileCacheEnabled, Cached)
			VALUES        (@LibraryId,@Name,@RelativePath,@TypeId,@DateModified,@Size,@Content,@ParentId,@Id, @DownloadCount, @DisplayName, @IndexDateModified,
				@FileCacheEnabled, @Cached)
		END

	SELECT @Id;

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

