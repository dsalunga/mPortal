CREATE PROCEDURE [GenericListColumnOption_GetChoices]
	(
		@ColumnId int
	)
AS
	SET NOCOUNT ON
	
	SELECT     o.Id, o.ColumnId, o.OptionTypeId, o.Rank, o.Caption, o.DefaultValue, ot.Id AS Expr1, ot.Label, ot.Template
	FROM         GenericListColumnOption AS o INNER JOIN
	                      GenericListColumnOptionType AS ot ON o.OptionTypeId = ot.Id
	WHERE     (o.ColumnId = @ColumnId)
	ORDER BY o.Rank
		
	RETURN