CREATE TABLE [dbo].[BIBLE_LBLA] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_LBL__BookC__1E6F845E] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_LBL__Chapt__1F63A897] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_LBL__Verse__2057CCD0] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_LBLA] PRIMARY KEY CLUSTERED ([Id] ASC)
);

