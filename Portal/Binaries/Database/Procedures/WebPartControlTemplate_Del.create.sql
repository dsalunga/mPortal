
-- Procedure WebPartControlTemplate_Del
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebPartControlTemplate_Del]
	(
		@PartControlTemplateId int
	)
AS
	SET NOCOUNT ON
	
	IF(@PartControlTemplateId > 0)
		DELETE FROM WebPartControlTemplate
		WHERE PartControlTemplateId=@PartControlTemplateId
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

