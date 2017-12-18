CREATE PROCEDURE [dbo].[WebPart_Set]
	(
		@PartId int = -1,
		@Name nvarchar(250),
		@Identity nvarchar(250),
		@Active int
	)
AS
	SET NOCOUNT ON
	
	IF(@PartId > 0)
		BEGIN
			-- Update
			
			UPDATE    WebPart
			SET              Name = @Name, [Identity] = @Identity, Active = @Active
			WHERE     (PartId = @PartId)
		END
	ELSE
		BEGIN
			-- Insert
			
			EXEC @PartId = WebObjects_NextId 'WebPart'
			
			INSERT INTO WebPart
			                      (Name, [Identity], Active, PartId)
			VALUES     (@Name,@Identity,@Active,@PartId)
		END
		
	SELECT @PartId
	
	RETURN