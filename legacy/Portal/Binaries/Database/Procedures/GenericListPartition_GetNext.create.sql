
-- Procedure GenericListPartition_GetNext
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [GenericListPartition_GetNext]
	(
		@ListId int,
		@PrePartitionId int = -1
	)
AS
	SET NOCOUNT ON
	DECLARE @Rank int
	
	if(@PrePartitionId = -1)
		begin
			SELECT TOP 1 Id FROM GenericListPartition WHERE ListId=@ListId ORDER BY Rank
		end
	else
		begin
			SET @Rank = (SELECT Rank FROM GenericListPartition WHERE Id = @PrePartitionId)
			SELECT     TOP 1 Id
			FROM         GenericListPartition
			WHERE     (ListId = @ListId) AND (Rank > @Rank)
			ORDER BY Rank
		end
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

