
-- Procedure IncidentType_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.IncidentType_Set
	(
		@Id int = -1,
		@Name nvarchar(500),
		@FollowStdSLA int,
		@Rank int,
		@InstanceId int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update

			UPDATE    IncidentType
			SET              Name = @Name, FollowStdSLA=@FollowStdSLA,
					Rank=@Rank, InstanceId=@InstanceId
			WHERE     (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert

			EXEC @Id = WebObject_NextId 'IncidentType';

			INSERT INTO IncidentType
			                      (Name, FollowStdSLA, Id, Rank, InstanceId)
			VALUES     (@Name,@FollowStdSLA,@Id, @Rank, @InstanceId)
		END

	SELECT @Id;

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

