CREATE TABLE [dbo].[EventCalendarRecurrences] (
    [RecurrenceId] INT            NOT NULL,
    [Name]         NVARCHAR (250) NOT NULL,
    [Rank]         INT            CONSTRAINT [DF_EventCalendarRecurrences_Rank] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_EventCalendarRecurrences] PRIMARY KEY CLUSTERED ([RecurrenceId] ASC)
);

