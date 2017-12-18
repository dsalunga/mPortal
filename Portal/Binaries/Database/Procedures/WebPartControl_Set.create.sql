
-- Procedure WebPartControl_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebPartControl_Set]
	(
		@PartControlId int = -1,
		@PartId int,
		@Name nvarchar(250),
		@Identity nvarchar(250),
		@ConfigFileName nvarchar(250),
		@PartAdminId int,
		@EntryPoint int,
		@ParentId int
	)
AS
	SET NOCOUNT ON
	
	IF(@PartControlId > 0)
		BEGIN
			-- Update
			UPDATE    WebPartControl
			SET              PartId = @PartId, Name = @Name, [Identity] = @Identity, ConfigFileName = @ConfigFileName,
						PartAdminId=@PartAdminId, EntryPoint=@EntryPoint, ParentId=@ParentId
			WHERE     (PartControlId = @PartControlId)
		END
	ELSE
		BEGIN
			-- Insert
			EXEC @PartControlId = WebObject_NextId 'WebPartControl';
			
			INSERT INTO WebPartControl
			                      (PartId, Name, [Identity], ConfigFileName, PartControlId, PartAdminId, EntryPoint,
								  ParentId)
			VALUES     (@PartId,@Name,@Identity,@ConfigFileName,@PartControlId, @PartAdminId, @EntryPoint, @ParentId)
		END
		
	SELECT @PartControlId;
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

