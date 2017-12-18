CREATE PROCEDURE MenuObject_Set
	(
		@Id int = -1,
		@MenuID int = -1,
		@ObjectId int,
		@RecordId int,
		
		@Horizontal int = null,
		@Width nvarchar(63) = '',
		@Height nvarchar(63) = '',
		@ParameterSetId int,
		@RenderMode int
	)
AS
	SET NOCOUNT ON

	if(@Id > 0)
		begin
			/* UPDATE */
			UPDATE    MenuObject
			SET              Width = @Width, Height = @Height, Horizontal = @Horizontal, MenuID = @MenuID, 
							ParameterSetId=@ParameterSetId, RenderMode=@RenderMode
			WHERE     (Id = @Id)
		end
	else
		begin
			/* INSERT */
			
			EXEC @Id = WebObject_NextId 'MenuObject';
			
			INSERT INTO MenuObject
			                      (Id, RecordId, ObjectId, Width, Height, Horizontal, MenuID, ParameterSetId, RenderMode)
			VALUES     (@Id, @RecordId,@ObjectId,@Width,@Height,@Horizontal,@MenuID, @ParameterSetId, @RenderMode)
		end

	SELECT @Id;
	
	RETURN