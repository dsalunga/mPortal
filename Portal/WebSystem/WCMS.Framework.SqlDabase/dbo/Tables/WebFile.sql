CREATE TABLE [dbo].[WebFile] (
    [FileId]   INT            NOT NULL,
    [FolderId] INT            NOT NULL,
    [ObjectId] INT            NOT NULL,
    [RecordId] INT            NOT NULL,
    [Name]     NVARCHAR (250) COLLATE Latin1_General_CI_AI NOT NULL,
    CONSTRAINT [PK_WebFile] PRIMARY KEY CLUSTERED ([FileId] ASC)
);

