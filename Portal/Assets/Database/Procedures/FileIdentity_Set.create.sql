
-- Procedure FileIdentity_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.FileIdentity_Set
	(
		@Id int = -1,
		@ObjectId int,
		@RecordId int,
		@LibraryId int,
		@FilePath nvarchar(4000),
		@Name nvarchar(500)
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update

			UPDATE    FileIdentity
			SET              ObjectId = @ObjectId, RecordId = @RecordId, LibraryId = @LibraryId, FilePath = @FilePath,
							 Name=@Name
			WHERE     (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert

			EXEC @Id = WebObject_NextId 'FileIdentity';

			INSERT INTO FileIdentity
								  (Id, ObjectId, RecordId, LibraryId, FilePath, Name)
			VALUES     (@Id,@ObjectId,@RecordId,@LibraryId,@FilePath, @Name)
		END

	SELECT @Id

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

