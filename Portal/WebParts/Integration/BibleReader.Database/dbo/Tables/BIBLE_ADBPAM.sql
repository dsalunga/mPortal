CREATE TABLE [dbo].[BIBLE_ADBPAM] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__TABLE_ADB__BookC__1AD3FDA4] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__TABLE_ADB__Chapt__1BC821DD] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__TABLE_ADB__Verse__1CBC4616] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_TABLE_ADBPAM] PRIMARY KEY CLUSTERED ([Id] ASC)
);

