CREATE PROCEDURE [dbo].[ExternalMember_Del]
	(
		@MemberId int
	)
AS
	SET NOCOUNT ON
	
	IF(@MemberId > 0)
		DELETE FROM [ExternalDB].dbo.Members
		WHERE MemberId = @MemberId
	
	RETURN