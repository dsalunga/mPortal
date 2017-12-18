CREATE TABLE [dbo].[GenericListColumnOptionType] (
    [Id]       INT            NOT NULL,
    [Label]    NVARCHAR (255) COLLATE Latin1_General_CI_AI NOT NULL,
    [Template] NTEXT          COLLATE Latin1_General_CI_AI NULL,
    CONSTRAINT [PK_GenericListColumnOptionType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

