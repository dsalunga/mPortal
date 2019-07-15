CREATE PROCEDURE dbo.Member_Del
	(
		@MemberId int
	)
AS
	SET NOCOUNT ON
	
	IF(@MemberId > 0)
		DELETE FROM Member
		WHERE MemberId = @MemberId
	
	RETURN