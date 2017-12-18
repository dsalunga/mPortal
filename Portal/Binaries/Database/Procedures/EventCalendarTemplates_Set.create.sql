
-- Procedure EventCalendarTemplates_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EventCalendarTemplates_Set]
	(
		@TemplateId int = -1,
		@Name nvarchar(250),
		@ForeColor nvarchar(10),
		@BackColor nvarchar(10),
		@ReminderHtml ntext,
		@SmsContent nvarchar(1000)
	)
AS
	SET NOCOUNT ON
	
	IF(@TemplateId > 0)
		BEGIN
			-- Update
			
			UPDATE    EventCalendarTemplates
			SET              Name = @Name, ReminderHtml = @ReminderHtml, ForeColor=@ForeColor, BackColor=@BackColor,
						SmsContent = @SmsContent
			WHERE     (TemplateId = @TemplateId)
		END
	ELSE
		BEGIN
			-- Insert
			EXEC @TemplateId = WebObjects_NextId 'EventCalendarTemplates'
			
			INSERT INTO EventCalendarTemplates
			                      (Name, TemplateId, ReminderHtml, ForeColor, BackColor, SmsContent)
			VALUES     (@Name,@TemplateId,@ReminderHtml, @ForeColor, @BackColor, @SmsContent)
		END
		
	SELECT @TemplateId
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

