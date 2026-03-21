CREATE TABLE [dbo].[BIBLE_CEV] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_CEV__BookC__76619304] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_CEV__Chapt__7755B73D] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_CEV__Verse__7849DB76] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_CEV] PRIMARY KEY CLUSTERED ([Id] ASC)
);

