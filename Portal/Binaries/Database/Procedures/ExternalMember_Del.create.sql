
-- Procedure ExternalMember_Del
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

