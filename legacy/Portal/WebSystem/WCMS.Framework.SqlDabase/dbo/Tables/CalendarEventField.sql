CREATE TABLE [dbo].[CalendarEventField] (
    [Id]          INT            NOT NULL,
    [Name]        NVARCHAR (250) NOT NULL,
    [FieldString] NVARCHAR (250) NOT NULL,
    CONSTRAINT [PK_LocalCalendarEventField] PRIMARY KEY CLUSTERED ([Id] ASC)
);

