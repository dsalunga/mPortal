CREATE TABLE [dbo].[EventCalendar] (
    [Id]     INT            NOT NULL,
    [Name]   NVARCHAR (250) COLLATE Latin1_General_CI_AI NOT NULL,
    [SiteId] INT            CONSTRAINT [DF__EventCalendar] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_EventCalendar] PRIMARY KEY CLUSTERED ([Id] ASC)
);

