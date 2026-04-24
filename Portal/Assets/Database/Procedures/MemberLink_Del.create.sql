
-- Procedure MemberLink_Del
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

