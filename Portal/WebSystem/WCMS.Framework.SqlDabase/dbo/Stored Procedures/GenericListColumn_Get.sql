CREATE PROCEDURE [GenericListColumn_Get]
	(
		@ListId int
	)
AS
	SET NOCOUNT ON
	SELECT     ri.Id, ri.RowId, ri.ColumnId, ri.Answer, sq.Label, sq.Rank
	FROM         GenericListField AS ri INNER JOIN
	                      GenericListColumn AS sq ON ri.ColumnId = sq.Id INNER JOIN
	                      GenericListRow AS r ON ri.RowId = r.Id
	WHERE     (r.ListId = @ListId)
	ORDER BY sq.Rank DESC
	RETURN