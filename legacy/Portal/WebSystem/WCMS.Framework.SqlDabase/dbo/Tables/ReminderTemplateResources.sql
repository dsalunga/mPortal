CREATE TABLE [dbo].[ReminderTemplateResources] (
    [TemplateResourceId] INT            NOT NULL,
    [TemplateId]         INT            NOT NULL,
    [Name]               NVARCHAR (250) NOT NULL,
    [FileName]           NVARCHAR (250) NOT NULL,
    [Identity]           NVARCHAR (250) NOT NULL,
    CONSTRAINT [PK_ReminderTemplateResources] PRIMARY KEY CLUSTERED ([TemplateResourceId] ASC)
);

