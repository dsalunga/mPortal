CREATE TABLE [dbo].[BIBLE_ESV] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_ESV__BookC__04AFB25B] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_ESV__Chapt__05A3D694] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_ESV__Verse__0697FACD] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_ESV] PRIMARY KEY CLUSTERED ([Id] ASC)
);

