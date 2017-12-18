CREATE PROCEDURE dbo.WebParameterSet_Get
	(
		@Id int = -1
	)
AS
	SET NOCOUNT ON

	SELECT     Id, Name, SchemaParameterName
	FROM         WebParameterSet
	WHERE     (@Id=-1 OR Id = @Id)

	RETURN