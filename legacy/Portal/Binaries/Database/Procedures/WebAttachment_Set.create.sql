
-- Procedure WebAttachment_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.WebAttachment_Set
	(
		@Id int = -1,
		@Name nvarchar(500),
		@FilePath nvarchar(500),
		@Size bigint,
		@DateUploaded datetime,
		@UserId int,
		@ObjectId int,
		@RecordId int,
		@BatchGuid nvarchar(50)
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update

			UPDATE       WebAttachment
			SET                Name = @Name, FilePath = @FilePath, Size = @Size, DateUploaded = @DateUploaded, UserId = @UserId, ObjectId = @ObjectId, 
							RecordId = @RecordId, BatchGuid=@BatchGuid
			WHERE        (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert

			EXEC @Id = WebObject_NextId 'WebAttachment';

			INSERT INTO WebAttachment
			                         (Name, FilePath, Size, DateUploaded, UserId, ObjectId, RecordId, Id,
							BatchGuid)
			VALUES        (@Name,@FilePath,@Size,@DateUploaded,@UserId,@ObjectId,@RecordId,@Id,
					@BatchGuid)
		END

	SELECT @Id;

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

