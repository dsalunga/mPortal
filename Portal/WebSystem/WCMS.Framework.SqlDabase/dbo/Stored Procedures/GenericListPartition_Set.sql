CREATE PROCEDURE [GenericListPartition_Set]
	(
		@PartitionId int = -1,
		@ListId int = -1,
		@Rank int,
		@Title nvarchar(255)
	)
AS
	SET NOCOUNT ON
	
	if(@PartitionId = -1)
		begin
			/* INSERT */
			
			INSERT INTO GenericListPartition
			                      (Rank, ListId, Title)
			VALUES     (@Rank, @ListId, @Title)
		end
	else
		begin
			/* UPDATE */
			
			UPDATE    GenericListPartition
			SET              Rank = @Rank, Title = @Title
			WHERE     (Id = @PartitionId)
		end
	RETURN