CREATE TABLE [dbo].[WebAttachment] (
    [Id]           INT            NOT NULL,
    [Name]         NVARCHAR (500) COLLATE Latin1_General_CI_AI NOT NULL,
    [FilePath]     NVARCHAR (500) COLLATE Latin1_General_CI_AI NOT NULL,
    [Size]         BIGINT         NOT NULL,
    [DateUploaded] DATETIME       NOT NULL,
    [UserId]       INT            NOT NULL,
    [ObjectId]     INT            NOT NULL,
    [RecordId]     INT            NOT NULL,
    [BatchGuid]    NVARCHAR (50)  COLLATE Latin1_General_CI_AI NOT NULL,
    CONSTRAINT [PK_WebAttachment] PRIMARY KEY CLUSTERED ([Id] ASC)
);

