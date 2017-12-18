CREATE TABLE [dbo].[ArticleList] (
    [ListId]     INT NOT NULL,
    [PageSize]   INT NOT NULL,
    [ObjectId]   INT NOT NULL,
    [RecordId]   INT NOT NULL,
    [TemplateId] INT NOT NULL,
    [SiteId]     INT NOT NULL,
    [FolderId]   INT CONSTRAINT [DF_ArticleList_FolderId] DEFAULT ((-1)) NOT NULL,
    [CommentOn]  INT NOT NULL,
    CONSTRAINT [PK_ArticleList] PRIMARY KEY CLUSTERED ([ListId] ASC)
);

