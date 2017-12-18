CREATE TABLE [dbo].[Reminders] (
    [ReminderId] INT            NOT NULL,
    [MemberId]   INT            NOT NULL,
    [TemplateId] INT            NOT NULL,
    [PhotoFile]  NVARCHAR (250) NOT NULL,
    CONSTRAINT [PK_Reminders] PRIMARY KEY CLUSTERED ([ReminderId] ASC)
);

