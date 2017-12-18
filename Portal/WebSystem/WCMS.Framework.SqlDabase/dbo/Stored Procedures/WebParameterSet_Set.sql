CREATE PROCEDURE dbo.WebParameterSet_Set
	(
		@Id int = -1,
		@Name nvarchar(250),
		@SchemaParameterName nvarchar(250)
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update

			UPDATE    WebParameterSet
			SET              Name = @Name, SchemaParameterName=@SchemaParameterName
			WHERE     (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert
			EXEC @Id = WebObject_NextId 'WebParameterSet';

			INSERT INTO WebParameterSet
			                      (Name, Id, SchemaParameterName)
			VALUES     (@Name,@Id, @SchemaParameterName)
		END

	SELECT @Id;

	RETURN