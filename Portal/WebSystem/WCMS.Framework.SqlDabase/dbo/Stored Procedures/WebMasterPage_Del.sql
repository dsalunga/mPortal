CREATE PROCEDURE [dbo].[WebMasterPage_Del]
	(
		@MasterPageId int
	)
AS
	SET NOCOUNT ON
	
	if(@MasterPageId > 0)
		BEGIN
			
			DELETE FROM WebMasterPage
			WHERE MasterPageId=@MasterPageId
			
		END
	
	RETURN