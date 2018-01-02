CREATE TABLE [dbo].[BIBLE_ABBTAG] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF_Bible_ABBTAG_BookCode] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF_Bible_ABBTAG_ChapterCode] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF_Bible_ABBTAG_VerseCode] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_Bible_ABBTAG] PRIMARY KEY CLUSTERED ([Id] ASC)
);

