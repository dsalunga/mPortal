CREATE TABLE [dbo].[BIBLE_HIL] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_HIL__BookC__15DA3E5D] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_HIL__Chapt__16CE6296] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_HIL__Verse__17C286CF] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_HIL] PRIMARY KEY CLUSTERED ([Id] ASC)
);

