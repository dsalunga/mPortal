
-- Procedure BibleReaderAccess_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.BibleReaderAccess_Set
	(
		@Id int = -1,
		@UserId int,
		@AppAccessCount int,
		@LastAccessed datetime
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update
			UPDATE       BibleReaderAccess
			SET                UserId = @UserId, AppAccessCount = @AppAccessCount, LastAccessed = @LastAccessed
			WHERE        (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert
			EXEC @Id = WebObject_NextId 'BibleReaderAccess';

			INSERT INTO BibleReaderAccess
			                         (UserId, AppAccessCount, LastAccessed, Id)
			VALUES        (@UserId,@AppAccessCount,@LastAccessed,@Id)
		END

	SELECT @Id;

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

