CREATE PROCEDURE dbo.FileVersion_Set
	(
		@Id int = -1,
		@FileId int,
		@VersionDate datetime,
		@Activity int,
		@UserId int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update
			
			UPDATE    FileVersion
			SET              FileId = @FileId, VersionDate = @VersionDate, Activity = @Activity, UserId = @UserId
			WHERE     (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert

			EXEC @Id = WebObject_NextId 'FileVersion';

			INSERT INTO FileVersion
								  (Id, FileId, VersionDate, Activity, UserId)
			VALUES     (@Id,@FileId,@VersionDate,@Activity,@UserId)
		END

	SELECT @Id;

	RETURN