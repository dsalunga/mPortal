CREATE TABLE [dbo].[ArticleColumn] (
    [ColumnId]   INT            NOT NULL,
    [Name]       NVARCHAR (500) NOT NULL,
    [TemplateId] INT            NOT NULL,
    [Id]         INT            NOT NULL,
    [IsSingle]   INT            NOT NULL,
    CONSTRAINT [PK_ArticleColumn] PRIMARY KEY CLUSTERED ([ColumnId] ASC)
);

