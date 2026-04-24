
-- Procedure GenericListPartition_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [GenericListPartition_Get]
	(
		@ListId int = -1,
		@PartitionId int = -1
	)
AS
	SET NOCOUNT ON
	
	
	if(@PartitionId > 0)
		begin
			SELECT     SP.Id, SP.ListId, SP.Rank, SP.Title, SP.Description, SP.ActionOptionId, SP.ActionPartitionId, SP.ActionOptionValue, S.Title AS SurveyTitle, 
			                      S.Description AS SurveyDescription, S.ShowPageCaption
			FROM         GenericListPartition AS SP INNER JOIN
			                      GenericList AS S ON SP.ListId = S.Id
			WHERE     (SP.Id = @PartitionId)
		end
	else
		begin
			SELECT     Id, ListId, Rank, Title, Description, ActionOptionId, ActionPartitionId, ActionOptionValue
					FROM         GenericListPartition
					WHERE     (@ListId = -1 OR ListId = @ListId)
					ORDER BY Rank
		end
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

