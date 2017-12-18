
-- Procedure GenericList_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [GenericList_Get]
	(
		@ListId int
	)
AS
	SET NOCOUNT ON
	
	if(@ListId > 0)
		begin
			SELECT * FROM GenericList WHERE Id = @ListId
		end
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

