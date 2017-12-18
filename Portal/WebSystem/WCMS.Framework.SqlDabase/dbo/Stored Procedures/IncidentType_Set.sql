CREATE PROCEDURE dbo.IncidentType_Set
	(
		@Id int = -1,
		@Name nvarchar(500),
		@FollowStdSLA int,
		@Rank int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update

			UPDATE    IncidentType
			SET              Name = @Name, FollowStdSLA=@FollowStdSLA,
					Rank=@Rank
			WHERE     (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert

			EXEC @Id = WebObject_NextId 'IncidentType';

			INSERT INTO IncidentType
			                      (Name, FollowStdSLA, Id, Rank)
			VALUES     (@Name,@FollowStdSLA,@Id, @Rank)
		END

	SELECT @Id;

	RETURN