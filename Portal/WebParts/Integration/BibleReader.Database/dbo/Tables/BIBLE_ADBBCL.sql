CREATE TABLE [dbo].[BIBLE_ADBBCL] (
    [CompositeCode] INT            NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [Id]            INT            NOT NULL,
    [BookCode]      INT            CONSTRAINT [DF__BIBLE_ADB__BookC__0D7A0286] DEFAULT ((-1)) NOT NULL,
    [ChapterCode]   INT            CONSTRAINT [DF__BIBLE_ADB__Chapt__0E6E26BF] DEFAULT ((-1)) NOT NULL,
    [VerseCode]     INT            CONSTRAINT [DF__BIBLE_ADB__Verse__0F624AF8] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BIBLE_ADBBCL] PRIMARY KEY CLUSTERED ([Id] ASC)
);

