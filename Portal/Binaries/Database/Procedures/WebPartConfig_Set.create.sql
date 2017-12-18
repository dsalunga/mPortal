
-- Procedure WebPartConfig_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebPartConfig_Set]
	(
		@PartConfigId int = -1,
		@PartId int,
		@Name nvarchar(250),
		@FileName nvarchar(250)
	)
AS
	SET NOCOUNT ON
	
	IF(@PartConfigId > 0)
		BEGIN
			-- Update
			
			UPDATE    WebPartConfig
			SET              PartId = @PartId, Name = @Name, FileName = @FileName
			WHERE     (PartConfigId = @PartConfigId)
		END
	ELSE
		BEGIN
			-- Insert
			
			EXEC @PartConfigId = WebObjects_NextId 'WebPartConfig'
			
			INSERT INTO WebPartConfig
			                      (PartId, Name, FileName, PartConfigId)
			VALUES     (@PartId,@Name,@FileName,@PartConfigId)
		END
		
	SELECT @PartConfigId
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

