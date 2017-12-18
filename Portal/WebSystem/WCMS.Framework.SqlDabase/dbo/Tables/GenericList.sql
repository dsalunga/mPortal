CREATE TABLE [dbo].[GenericList] (
    [Id]              INT             IDENTITY (1, 1) NOT NULL,
    [Title]           NVARCHAR (255)  COLLATE Latin1_General_CI_AI NOT NULL,
    [Description]     NTEXT           COLLATE Latin1_General_CI_AI NULL,
    [IsActive]        INT             NOT NULL,
    [EndingText]      NVARCHAR (2000) COLLATE Latin1_General_CI_AI NULL,
    [ShowPageCaption] INT             NOT NULL,
    CONSTRAINT [PK_GenericList] PRIMARY KEY CLUSTERED ([Id] ASC)
);

