CREATE PROCEDURE dbo.WebSkin_Set
	(
		@Id int = -1,
		@Name nvarchar(500),
		@Rank int,
		@ObjectId int,
		@RecordId int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update

			UPDATE    WebSkin
			SET              Name = @Name, ObjectId=@ObjectId, RecordId=@RecordId, Rank=@Rank
			WHERE     (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert

			EXEC @Id = WebObject_NextId 'WebSkin';

			INSERT INTO WebSkin
			                      (Name, Id, ObjectId, RecordId, Rank)
			VALUES     (@Name,@Id, @ObjectId, @RecordId, @Rank)
		END

	SELECT @Id;

	RETURN