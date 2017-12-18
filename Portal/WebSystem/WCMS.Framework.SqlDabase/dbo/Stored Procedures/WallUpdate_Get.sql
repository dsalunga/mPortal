CREATE PROCEDURE dbo.WallUpdate_Get
	(
		@Id int = -1,
		@UpdateObjectId int = -2,
		@UpdateRecordId int = -2,
		@ByObjectId int = -2,
		@ByRecordId int = -2,
		@EventTypeId int = -2,
		@UpdateDateStart datetime = NULL,
		@UpdateDateEnd datetime = NULL
	)
AS
	SET NOCOUNT ON

	SELECT        Id, UpdateRecordId, UpdateObjectId, ByRecordId, ByObjectId, [Content], UpdateDate,
			EventTypeId
	FROM            WallUpdate
	WHERE	(@Id=-1 OR Id=@Id)

		AND	(@UpdateDateStart IS NULL OR UpdateDate>=@UpdateDateStart)
		AND (@UpdateDateEnd IS NULL OR UpdateDate<=@UpdateDateEnd)

		AND (@UpdateObjectId=-2 OR UpdateObjectId=@UpdateObjectId)
		AND (@UpdateRecordId=-2 OR UpdateRecordId=@UpdateRecordId)

		AND (@ByObjectId=-2 OR ByObjectId=@ByObjectId)
		AND (@ByRecordId=-2 OR ByRecordId=@ByRecordId)

		AND (@EventTypeId = -2 OR EventTypeId=@EventTypeId)
	ORDER BY UpdateDate DESC

	RETURN