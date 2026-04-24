
-- Procedure WallUpdate_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.WallUpdate_Set
	(
		@Id int = -1,
		@UpdateRecordId int,
		@UpdateObjectId int,
		@ByObjectId int,
		@ByRecordId int,
		@Content ntext,
		@UpdateDate datetime,
		@EventTypeId int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update

			UPDATE       WallUpdate
			SET              UpdateRecordId = @UpdateRecordId, UpdateObjectId = @UpdateObjectId, ByRecordId = @ByRecordId, ByObjectId = @ByObjectId, [Content] = @Content, 
			                 UpdateDate = @UpdateDate, EventTypeId=@EventTypeId
			WHERE        (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert

			EXEC @Id = WebObject_NextId 'WallUpdate';

			INSERT INTO WallUpdate
						(Id, UpdateRecordId, UpdateObjectId, ByRecordId, ByObjectId, [Content], UpdateDate, EventTypeId)
			VALUES      (@Id,@UpdateRecordId,@UpdateObjectId,@ByRecordId,@ByObjectId,@Content,@UpdateDate, @EventTypeId)
		END

	SELECT @Id;

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

