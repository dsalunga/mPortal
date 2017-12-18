﻿CREATE PROCEDURE [GenericListRow_Set]
	(
		@RowId int = -1,
		@ListId int = -1,
		@IsCompleted int
	)
AS
	SET NOCOUNT ON
	
	if(@ListId = -1)
		begin
			/* INSERT */
			INSERT INTO GenericListRow
			                      (ListId, IsCompleted, DateTimeTaken)
			VALUES     (@ListId, @IsCompleted, GETDATE())
			
			SELECT @@IDENTITY AS RowId
		end
	else
		begin
			/* UPDATE */
			
			UPDATE    GenericListRow
			SET              IsCompleted = @IsCompleted
			WHERE Id = @RowId
		end
	
	RETURN