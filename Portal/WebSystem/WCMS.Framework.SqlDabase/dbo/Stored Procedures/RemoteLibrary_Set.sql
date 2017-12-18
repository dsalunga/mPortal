CREATE PROCEDURE dbo.RemoteLibrary_Set
	(
		@Id int = -1,
		@Name varchar(300),
		@SourceTypeId int,
		@BaseAddress nvarchar(500),
		@DisplayBaseAddress nvarchar(500),
		@UserName nvarchar(50),
		@Password nvarchar(50),
		@LastIndexDate datetime,
		@Active int,
		@DownloadCountSince datetime,
		@FileCacheEnabled int,
		@FileCacheFolder nvarchar(500),
		@FileCacheMinDownloadCount int,
		@FileCacheCeilingSize int, 
		@FileCacheMaxSize int,
		@FileCacheMinDiskFreeMB int,
		@Size bigint
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- UPDATE
			UPDATE       RemoteLibrary
			SET		Name = @Name, SourceTypeId = @SourceTypeId, BaseAddress = @BaseAddress, UserName = @UserName, Password = @Password, 
					LastIndexDate = @LastIndexDate, Active=@Active, DisplayBaseAddress=@DisplayBaseAddress, 
					DownloadCountSince=@DownloadCountSince, FileCacheEnabled=@FileCacheEnabled, FileCacheFolder=@FileCacheFolder,
					FileCacheMinDownloadCount=@FileCacheMinDownloadCount, FileCacheCeilingSize=@FileCacheCeilingSize,
					FileCacheMaxSize=@FileCacheMaxSize, FileCacheMinDiskFreeMB=@FileCacheMinDiskFreeMB, Size=@Size
			WHERE Id=@Id
		END
	ELSE
		BEGIN
			-- INSERT

			EXEC @Id = WebObject_NextId 'RemoteLibrary';

			INSERT INTO RemoteLibrary
			                         (Name, SourceTypeId, BaseAddress, UserName, Password, LastIndexDate, Id, Active, DisplayBaseAddress,
						DownloadCountSince, FileCacheEnabled, FileCacheFolder, FileCacheMinDownloadCount, FileCacheCeilingSize, 
						FileCacheMaxSize, FileCacheMinDiskFreeMB, Size)
			VALUES        (@Name,@SourceTypeId,@BaseAddress,@UserName,@Password,@LastIndexDate,@Id, @Active, @DisplayBaseAddress,
						@DownloadCountSince, @FileCacheEnabled, @FileCacheFolder, @FileCacheMinDownloadCount, @FileCacheCeilingSize, 
						@FileCacheMaxSize, @FileCacheMinDiskFreeMB, @Size)
		END

	SELECT @Id;

	RETURN