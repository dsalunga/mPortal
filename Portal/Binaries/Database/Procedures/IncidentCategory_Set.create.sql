
-- Procedure IncidentCategory_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.IncidentCategory_Set
	(
		@Id int = -1,
		@Name nvarchar(500),
		@GroupId int,
		@Description nvarchar(4000),
		@Rank int,
		@InstanceId int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update

			UPDATE    IncidentCategory
			SET              Name = @Name, GroupId = @GroupId, Description = @Description,
					Rank=@Rank, InstanceId=@InstanceId
			WHERE     (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert

			EXEC @Id = WebObject_NextId 'IncidentCategory';

			INSERT INTO IncidentCategory
			                      (Name, GroupId, Description, Id, Rank, InstanceId)
			VALUES     (@Name,@GroupId,@Description,@Id, @Rank, @InstanceId)
		END

	SELECT @Id;

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

