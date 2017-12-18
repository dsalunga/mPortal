CREATE TABLE [dbo].[ArticleTemplate] (
    [Id]               INT             NOT NULL,
    [Name]             NVARCHAR (250)  NOT NULL,
    [Date]             DATETIME        NOT NULL,
    [File]             NVARCHAR (250)  NOT NULL,
    [ImageUrl]         NVARCHAR (250)  NOT NULL,
    [ListItemTemplate] NVARCHAR (2500) COLLATE Latin1_General_CI_AI CONSTRAINT [DF_ArticleTemplate_ItemTemplate] DEFAULT ('') NOT NULL,
    [ListTemplate]     NVARCHAR (2500) COLLATE Latin1_General_CI_AI CONSTRAINT [DF_ArticleTemplate_ListTemplate] DEFAULT ('') NOT NULL,
    [DetailsTemplate]  NVARCHAR (2500) COLLATE Latin1_General_CI_AI CONSTRAINT [DF_ArticleTemplate_DetailsTemplate] DEFAULT ('') NOT NULL,
    [DateFormat]       NVARCHAR (500)  CONSTRAINT [DF__ArticleTe__DateFormat] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_Templates] PRIMARY KEY CLUSTERED ([Id] ASC)
);

