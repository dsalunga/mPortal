
-- Procedure IncidentInstance_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.IncidentInstance_Set
	(
		@Id int = -1,
		@Name nvarchar(500),
		@IncidentPrefix nvarchar(500),
		@BaseGroup nvarchar(500),
		@SupportGroupPath nvarchar(500),
		@SLAHighDuration int,
		@SLALowDuration int,
		@SLANormalDuration int,
		@SLAWarningPercentage decimal(5,4)
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update

			UPDATE    IncidentInstance
			SET              Name = @Name, IncidentPrefix=@IncidentPrefix, BaseGroup=@BaseGroup, SupportGroupPath=@SupportGroupPath,
				SLAHighDuration=@SLAHighDuration, SLALowDuration=@SLALowDuration, SLANormalDuration=SLANormalDuration, SLAWarningPercentage=@SLAWarningPercentage
			WHERE     (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert

			EXEC @Id = WebObject_NextId 'IncidentInstance';

			INSERT INTO IncidentInstance
			                      (Id,Name,IncidentPrefix, BaseGroup, SupportGroupPath, SLAHighDuration, SLALowDuration, SLANormalDuration, SLAWarningPercentage)
			VALUES     (@Id,@Name,@IncidentPrefix, @BaseGroup, @SupportGroupPath, @SLAHighDuration, @SLALowDuration, @SLANormalDuration, @SLAWarningPercentage)
		END

	SELECT @Id;

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

