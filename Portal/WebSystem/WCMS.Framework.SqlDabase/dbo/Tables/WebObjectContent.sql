CREATE TABLE [dbo].[WebObjectContent] (
    [ObjectContentId] INT NOT NULL,
    [ObjectId]        INT NOT NULL,
    [ContentId]       INT NOT NULL,
    [RecordId]        INT NOT NULL,
    CONSTRAINT [PK_WebObjectContents] PRIMARY KEY CLUSTERED ([ObjectContentId] ASC)
);

