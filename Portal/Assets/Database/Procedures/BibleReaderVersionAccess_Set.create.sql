
-- Procedure BibleReaderVersionAccess_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.BibleReaderVersionAccess_Set
	(
		@Id int = -1,
		@BibleAccessId int,
		@BibleVersionId int,
		@VersionAccessCount int,
		@BibleVersionName nvarchar(250),
		@LastAccessed datetime
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- UPDATE

			UPDATE       BibleReaderVersionAccess
			SET                BibleAccessId = @BibleAccessId, BibleVersionId = @BibleVersionId, BibleVersionName = @BibleVersionName, 
					LastAccessed = @LastAccessed, VersionAccessCount=@VersionAccessCount
			WHERE        (Id = @Id)
		END
	ELSE
		BEGIN
			-- INSERT

			EXEC @Id=WebObject_NextId 'BibleReaderVersionAccess';

			INSERT INTO BibleReaderVersionAccess
			                         (BibleAccessId, BibleVersionId, BibleVersionName, LastAccessed, Id, VersionAccessCount)
			VALUES        (@BibleAccessId,@BibleVersionId,@BibleVersionName,@LastAccessed,@Id, @VersionAccessCount)
		END

	SELECT @Id;

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

