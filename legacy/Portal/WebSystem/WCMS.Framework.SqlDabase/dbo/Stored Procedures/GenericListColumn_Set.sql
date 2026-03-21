create PROCEDURE [GenericListColumn_Set]
	(
		@ColumnId int = -1,
		@PartitionId int,
		@ListId int = -1,
		@Rank int,
		@Label nvarchar(2000),
		@IsHorizontal int,
		@IsRequired int
	)
AS
	SET NOCOUNT ON
	
	if(@ColumnId = -1)
		begin
			/* INSERT */
			
			INSERT INTO GenericListColumn
			                      (ListId, PartitionId, Rank, Label, IsHorizontal, IsRequired)
			VALUES     (@ListId, @PartitionId, @Rank, @Label, @IsHorizontal, @IsRequired)
			
			SELECT @@IDENTITY AS ColumnId
		end
	else
		begin
			/* UPDATE */
			
			UPDATE    GenericListColumn
			SET              PartitionId = @PartitionId, Rank = @Rank, Label = @Label, IsHorizontal = @IsHorizontal, IsRequired = @IsRequired
			WHERE     (Id = @ColumnId)
			
		end
	
	RETURN