CREATE PROCEDURE [dbo].[MemberLink_Del]
	(
		@MemberLinkId int
	)
AS
	SET NOCOUNT ON
	
	IF(@MemberLinkId > 0)
		BEGIN
			DELETE FROM MemberLink
			WHERE MemberLinkId=@MemberLinkId
		END
	
	RETURN