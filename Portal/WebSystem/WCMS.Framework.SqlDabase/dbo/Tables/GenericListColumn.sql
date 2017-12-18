CREATE TABLE [dbo].[GenericListColumn] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [ListId]       INT             NOT NULL,
    [PartitionId]  INT             NOT NULL,
    [Rank]         INT             NOT NULL,
    [Label]        NVARCHAR (2500) COLLATE Latin1_General_CI_AI NOT NULL,
    [IsHorizontal] INT             NOT NULL,
    [IsRequired]   INT             NOT NULL,
    CONSTRAINT [PK_GenericListColumn] PRIMARY KEY CLUSTERED ([Id] ASC)
);

