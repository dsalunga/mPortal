CREATE TABLE [dbo].[BibleBookName] (
    [BookNameCode] INT            NOT NULL,
    [BookCode]     INT            NOT NULL,
    [Name]         NVARCHAR (500) NOT NULL,
    [MaxChapter]   INT            NOT NULL,
    [Id]           INT            NOT NULL,
    [ShortName]    NVARCHAR (50)  CONSTRAINT [DF_BookName_ShortBookName] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_BookName] PRIMARY KEY CLUSTERED ([Id] ASC)
);

