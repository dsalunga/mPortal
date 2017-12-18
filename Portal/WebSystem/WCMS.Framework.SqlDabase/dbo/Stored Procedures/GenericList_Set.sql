CREATE PROCEDURE [GenericList_Set]
	(
		@ListId int = -1,
		@Title nvarchar(255),
		@Description nvarchar(2000),
		@EndingText nvarchar(2000),
		@IsActive int,
		@ShowPageCaption int
	)
AS
	SET NOCOUNT ON
	
	if(@ListId = -1)
		begin
			/* INSERT */
			
			INSERT INTO GenericList
			                      (Title, Description, IsActive, EndingText, ShowPageCaption)
			VALUES     (@Title, @Description, @IsActive, @EndingText, @ShowPageCaption)
			
			SELECT @@IDENTITY AS ListId
		end
	else
		begin
			/* UPDATE */
			
			UPDATE    GenericList
			SET              Title = @Title, Description = @Description, IsActive = @IsActive, EndingText=@EndingText, ShowPageCaption=@ShowPageCaption
			WHERE     (Id = @ListId)
		end
	
	RETURN