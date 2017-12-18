CREATE TABLE [dbo].[ReminderTemplates] (
    [TemplateId]   INT            NOT NULL,
    [Name]         NVARCHAR (250) NOT NULL,
    [TemplateFile] NVARCHAR (250) NOT NULL,
    CONSTRAINT [PK_ReminderTemplates] PRIMARY KEY CLUSTERED ([TemplateId] ASC)
);

