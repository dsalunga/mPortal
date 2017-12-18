CREATE PROCEDURE dbo.IncidentCategory_Set
	(
		@Id int = -1,
		@Name nvarchar(500),
		@GroupId int,
		@Description nvarchar(4000),
		@Rank int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update

			UPDATE    IncidentCategory
			SET              Name = @Name, GroupId = @GroupId, Description = @Description,
					Rank=@Rank
			WHERE     (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert

			EXEC @Id = WebObject_NextId 'IncidentCategory';

			INSERT INTO IncidentCategory
			                      (Name, GroupId, Description, Id, Rank)
			VALUES     (@Name,@GroupId,@Description,@Id, @Rank)
		END

	SELECT @Id;

	RETURN