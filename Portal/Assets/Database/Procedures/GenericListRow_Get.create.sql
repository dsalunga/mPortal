
-- Procedure GenericListRow_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [GenericListRow_Get]
	(
		@ListId int = -1,
		@RowId int = -1
	)
AS
	SET NOCOUNT ON
	
	if(@RowId = -1)
		begin
			SELECT     Id, DateTimeTaken
			FROM         GenericListRow
			WHERE     (IsCompleted = 1) AND (ListId = @ListId)
			ORDER BY DateTimeTaken;
		end
	else
		begin
			SELECT     R.Id, R.RowId, R.ColumnId, R.Answer, Q.Label
			FROM         GenericListField AS R INNER JOIN
			                      GenericListColumn AS Q ON R.ColumnId = Q.Id INNER JOIN
			                      GenericListPartition AS P ON Q.PartitionId = P.Id
			WHERE     (Q.ListId = @ListId) AND (R.RowId = @RowId) AND (P.ListId = @ListId)
			ORDER BY P.Rank, Q.Rank
		end
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

