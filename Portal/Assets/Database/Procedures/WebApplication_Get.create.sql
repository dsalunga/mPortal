
-- Procedure WebApplication_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebApplication_Get]
	@Id int = -1
AS
	SELECT Id, Name, AppKey, IpAddresses FROM WebApplication
RETURN 0
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

