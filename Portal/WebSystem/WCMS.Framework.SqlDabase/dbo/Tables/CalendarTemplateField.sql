CREATE TABLE [dbo].[CalendarTemplateField] (
    [Id]         INT NOT NULL,
    [FieldId]    INT NOT NULL,
    [TemplateId] INT NOT NULL,
    CONSTRAINT [PK_LocalCalendarTemplateField] PRIMARY KEY CLUSTERED ([Id] ASC)
);

