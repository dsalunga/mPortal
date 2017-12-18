CREATE TABLE [dbo].[WebObjectSecurity] (
    [Id]               INT NOT NULL,
    [ObjectId]         INT NOT NULL,
    [RecordId]         INT NOT NULL,
    [SecurityObjectId] INT NOT NULL,
    [SecurityRecordId] INT NOT NULL,
    [Public]           INT CONSTRAINT [DF_WebObjectSecurity_Public] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_WebObjectAdmin] PRIMARY KEY CLUSTERED ([Id] ASC)
);

