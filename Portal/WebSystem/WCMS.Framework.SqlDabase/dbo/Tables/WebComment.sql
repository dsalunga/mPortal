CREATE TABLE [dbo].[WebComment] (
    [Id]          INT            NOT NULL,
    [Content]     NTEXT          COLLATE Latin1_General_CI_AI NOT NULL,
    [UserId]      INT            NOT NULL,
    [ObjectId]    INT            NOT NULL,
    [RecordId]    INT            NOT NULL,
    [DateCreated] DATETIME       NOT NULL,
    [ParentId]    INT            CONSTRAINT [DF_WebComment_ParentId] DEFAULT ((-1)) NOT NULL,
    [UserName]    NVARCHAR (500) CONSTRAINT [DF__WebCommen__UserName] DEFAULT ('') NOT NULL,
    [UserEmail]   NVARCHAR (500) CONSTRAINT [DF__WebCommen__UserEmail] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_WebComment] PRIMARY KEY CLUSTERED ([Id] ASC)
);

