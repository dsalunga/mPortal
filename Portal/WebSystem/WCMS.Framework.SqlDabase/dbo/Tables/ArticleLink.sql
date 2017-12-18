CREATE TABLE [dbo].[ArticleLink] (
    [Id]        INT             NOT NULL,
    [Rank]      INT             NOT NULL,
    [Style]     NVARCHAR (2500) NOT NULL,
    [ObjectId]  INT             NOT NULL,
    [RecordId]  INT             NOT NULL,
    [ArticleId] INT             NOT NULL,
    [SiteId]    INT             NOT NULL,
    [CommentOn] INT             CONSTRAINT [DF__ArticleLi__Comment] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_ArticleLink] PRIMARY KEY CLUSTERED ([Id] ASC)
);

