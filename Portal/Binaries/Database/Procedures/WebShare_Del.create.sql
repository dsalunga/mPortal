
-- Procedure WebShare_Del
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [WebShare_Del] 
	-- Add the parameters for the stored procedure here
	@Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF(@Id > 0)
		DELETE FROM WebShare
		WHERE Id=@Id;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

