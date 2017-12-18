CREATE TABLE [dbo].[GenericListRow] (
    [Id]            INT      IDENTITY (1, 1) NOT NULL,
    [ListId]        INT      NOT NULL,
    [IsCompleted]   INT      NOT NULL,
    [DateTimeTaken] DATETIME NOT NULL,
    CONSTRAINT [PK_GenericListRow] PRIMARY KEY CLUSTERED ([Id] ASC)
);

