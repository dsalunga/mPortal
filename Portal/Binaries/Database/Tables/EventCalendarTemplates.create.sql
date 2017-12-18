SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventCalendarTemplates]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EventCalendarTemplates](
	[TemplateId] [int] NOT NULL,
	[Name] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ReminderHtml] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ForeColor] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[BackColor] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SmsContent] [nvarchar](1000) COLLATE Latin1_General_CI_AI NOT NULL,
 CONSTRAINT [PK_EventCalendarTemplates] PRIMARY KEY CLUSTERED 
(
	[TemplateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_EventCalendarTemplates_SmsContent]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventCalendarTemplates] ADD  CONSTRAINT [DF_EventCalendarTemplates_SmsContent]  DEFAULT ('') FOR [SmsContent]
END

GO
