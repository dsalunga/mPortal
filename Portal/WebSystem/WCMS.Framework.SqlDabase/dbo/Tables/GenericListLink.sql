CREATE TABLE [dbo].[GenericListLink] (
    [Id]       INT IDENTITY (1, 1) NOT NULL,
    [RecordId] INT NOT NULL,
    [ObjectId] INT NOT NULL,
    [ListId]   INT NOT NULL,
    [SiteId]   INT NOT NULL,
    CONSTRAINT [PK_GenericListLink] PRIMARY KEY CLUSTERED ([Id] ASC)
);

