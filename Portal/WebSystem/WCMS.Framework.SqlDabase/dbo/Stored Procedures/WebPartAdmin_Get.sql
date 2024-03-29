﻿CREATE PROCEDURE [dbo].[WebPartAdmin_Get]
	(
		@PartAdminId int = -1,
		@ParentId int = -2,
		@PartId int = -1,
		@Name nvarchar(500) = NULL
	)
AS
	SET NOCOUNT ON
	
	SELECT  PartAdminId, PartId, Name, FileName, ParentId, Active, Visible,
			InSiteContext, TemplateEngineId
	FROM            WebPartAdmin
	WHERE        
			(@PartAdminId=-1 OR PartAdminId = @PartAdminId)
			AND (@ParentId=-2 OR ParentId=@ParentId)
			AND (@PartId=-1 OR PartId=@PartId)
			AND (@Name IS NULL OR Name=@Name)
	
	RETURN