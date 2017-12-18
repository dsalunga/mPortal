CREATE TABLE [dbo].[GenericListColumnOption] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [ColumnId]     INT             NOT NULL,
    [OptionTypeId] INT             NOT NULL,
    [Rank]         INT             NOT NULL,
    [Caption]      NVARCHAR (2000) COLLATE Latin1_General_CI_AI NULL,
    [DefaultValue] INT             NULL,
    CONSTRAINT [PK_GenericListColumnOption] PRIMARY KEY CLUSTERED ([Id] ASC)
);

