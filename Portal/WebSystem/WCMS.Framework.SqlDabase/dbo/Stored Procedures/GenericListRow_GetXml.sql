create PROCEDURE [GenericListRow_GetXml]
	(
		@ListId int
	)
AS
	SET NOCOUNT ON
	
	SELECT     CAST(R.DateTimeTaken AS varchar(64)) AS [Date & Time], RI.RowId AS [Response ID], Q.Label AS Question, RI.Answer
	FROM         GenericListField AS RI INNER JOIN
	                      GenericListColumn AS Q ON RI.ColumnId = Q.Id INNER JOIN
	                      GenericListPartition AS P ON Q.PartitionId = P.Id INNER JOIN
	                      GenericListRow AS R ON RI.RowId = R.Id
	WHERE     (Q.ListId = @ListId) AND (P.ListId = @ListId)
	ORDER BY [Response ID], P.Rank, Q.Rank
			
	RETURN