CREATE TABLE [dbo].[BIBLE_DARBY] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_DAR__BookC__793DFFAF] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_DAR__Chapt__7A3223E8] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_DAR__Verse__7B264821] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_DARBY] PRIMARY KEY CLUSTERED ([Id] ASC)
);

