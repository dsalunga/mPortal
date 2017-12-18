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