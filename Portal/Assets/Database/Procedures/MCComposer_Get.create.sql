
-- Procedure MCComposer_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MCComposer_Get]
	@Id int = -1,
	@CompetitionId INT = -2
AS
	SET NOCOUNT ON

	SELECT        Id, Name, Entry, Locale, Work, Description, PhotoFile, NickName, Active, CompetitionId
	FROM            MCComposer
	WHERE        (@Id=-1 OR Id = @Id)
		AND (@CompetitionId=-2 OR CompetitionId=@CompetitionId)
	ORDER BY Name

RETURN 0
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

