CREATE TABLE [dbo].[GenericListField] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [RowId]    INT            NOT NULL,
    [ColumnId] INT            NOT NULL,
    [Answer]   NVARCHAR (MAX) COLLATE Latin1_General_CI_AI NULL,
    CONSTRAINT [PK_GenericListField] PRIMARY KEY CLUSTERED ([Id] ASC)
);

