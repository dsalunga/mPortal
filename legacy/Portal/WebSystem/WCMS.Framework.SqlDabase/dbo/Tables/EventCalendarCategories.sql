CREATE TABLE [dbo].[EventCalendarCategories] (
    [CategoryId] INT            NOT NULL,
    [Name]       NVARCHAR (250) NOT NULL,
    [TemplateId] INT            CONSTRAINT [DF_LocalCalendarCategories_TemplateId] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_EventCalendarCategories] PRIMARY KEY CLUSTERED ([CategoryId] ASC)
);

