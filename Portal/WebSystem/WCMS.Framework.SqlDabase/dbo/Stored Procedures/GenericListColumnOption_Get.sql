CREATE PROCEDURE [GenericListColumnOption_Get]
	(
		@ColumnId int
	)
AS
	SET NOCOUNT ON
	
	SELECT     o.Id, o.ColumnId, o.OptionTypeId, o.Rank, o.Caption, o.DefaultValue, ot.Label
	FROM         GenericListColumnOption AS o LEFT OUTER JOIN
	                      GenericListColumnOptionType AS ot ON o.OptionTypeId = ot.Id
	WHERE     (o.ColumnId = @ColumnId)
	ORDER BY o.Rank DESC
	
	RETURN