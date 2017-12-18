
-- Procedure EventCalendarTemplates_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EventCalendarTemplates_Get]
	(
		@TemplateId int = -1
	)
AS
	SET NOCOUNT ON
	
	SELECT     TemplateId, Name, ReminderHtml, ForeColor, BackColor, ReminderHtml, SmsContent
	FROM         EventCalendarTemplates
	WHERE
		(@TemplateId = -1 OR
			TemplateId=@TemplateId)
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

