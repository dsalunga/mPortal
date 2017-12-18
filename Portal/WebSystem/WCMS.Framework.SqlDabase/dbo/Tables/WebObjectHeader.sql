CREATE TABLE [dbo].[WebObjectHeader] (
    [ObjectHeaderId] INT NOT NULL,
    [ObjectId]       INT NOT NULL,
    [RecordId]       INT NOT NULL,
    [TextResourceId] INT NOT NULL,
    CONSTRAINT [PK_WebObjectTextResources] PRIMARY KEY CLUSTERED ([ObjectHeaderId] ASC)
);

