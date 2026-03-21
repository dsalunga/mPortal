CREATE PROCEDURE [GenericListColumnOption_Set]
	(
		@OptionId int = -1,
		@ColumnId int = -1,
		@OptionTypeId int,
		@Rank int,
		@Caption ntext,
		@DefaultValue int
	)
AS
	SET NOCOUNT ON
	
	if(@OptionID = -1)
		begin
			/* INSERT */
			
			INSERT INTO GenericListColumnOption
			                      (ColumnId, OptionTypeId, Rank, Caption, DefaultValue)
			VALUES     (@ColumnId,@OptionTypeId,@Rank,@Caption,@DefaultValue)
			
		end
	else
		begin
			/* UPDATE */
			
			UPDATE    GenericListColumnOption
			SET              OptionTypeId = @OptionTypeID, Rank = @Rank, Caption = @Caption, DefaultValue = @DefaultValue
			WHERE     (Id = @OptionId)
		end
	
	RETURN