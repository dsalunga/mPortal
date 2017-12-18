CREATE TABLE [dbo].[Job] (
    [Id]          INT             NOT NULL,
    [Title]       NVARCHAR (2000) COLLATE Latin1_General_CI_AI NOT NULL,
    [Description] NVARCHAR (MAX)  COLLATE Latin1_General_CI_AI NOT NULL,
    CONSTRAINT [PK_Job] PRIMARY KEY CLUSTERED ([Id] ASC)
);

