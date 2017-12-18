CREATE TABLE [dbo].[GenericListPartition] (
    [Id]                INT             IDENTITY (1, 1) NOT NULL,
    [ListId]            INT             NOT NULL,
    [Rank]              INT             NOT NULL,
    [Title]             NVARCHAR (256)  COLLATE Latin1_General_CI_AI NULL,
    [Description]       NVARCHAR (2000) COLLATE Latin1_General_CI_AI NULL,
    [ActionOptionId]    INT             NULL,
    [ActionPartitionId] INT             NULL,
    [ActionOptionValue] NVARCHAR (50)   COLLATE Latin1_General_CI_AI NULL,
    CONSTRAINT [PK_GenericListPartition] PRIMARY KEY CLUSTERED ([Id] ASC)
);

