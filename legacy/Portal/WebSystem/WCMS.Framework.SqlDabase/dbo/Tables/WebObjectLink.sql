CREATE TABLE [dbo].[WebObjectLink] (
    [Id]            INT NOT NULL,
    [LeftRecordId]  INT NOT NULL,
    [LeftObjectId]  INT NOT NULL,
    [RightRecordId] INT NOT NULL,
    [RightObjectId] INT NOT NULL,
    CONSTRAINT [PK_WebLink] PRIMARY KEY CLUSTERED ([Id] ASC)
);

