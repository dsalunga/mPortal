CREATE TABLE [dbo].[EventCalendarTemplates] (
    [TemplateId]   INT             NOT NULL,
    [Name]         NVARCHAR (250)  NOT NULL,
    [ReminderHtml] NTEXT           NOT NULL,
    [ForeColor]    NVARCHAR (10)   NOT NULL,
    [BackColor]    NVARCHAR (10)   NOT NULL,
    [SmsContent]   NVARCHAR (1000) COLLATE Latin1_General_CI_AI CONSTRAINT [DF_EventCalendarTemplates_SmsContent] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_EventCalendarTemplates] PRIMARY KEY CLUSTERED ([TemplateId] ASC)
);

