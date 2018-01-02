CREATE TABLE [dbo].[BIBLE_MBBTAG] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_MBB__BookC__24285DB4] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_MBB__Chapt__251C81ED] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_MBB__Verse__2610A626] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_MBBTAG] PRIMARY KEY CLUSTERED ([Id] ASC)
);

