CREATE TABLE [dbo].[MusicEntry] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [MusicId]      INT            CONSTRAINT [DF__MusicEntr__MusicId] DEFAULT ((-1)) NOT NULL,
    [EntryTypeId]  INT            CONSTRAINT [DF__MusicEntr__EntryTypeId] DEFAULT ((-1)) NOT NULL,
    [FileName]     NVARCHAR (500) CONSTRAINT [DF__MusicEntr__FileName] DEFAULT ('') NOT NULL,
    [Tags]         NVARCHAR (MAX) CONSTRAINT [DF__MusicEntry__Tags] DEFAULT ('') NOT NULL,
    [DateModified] DATETIME       CONSTRAINT [DF__MusicEntr__DateModified] DEFAULT (getdate()) NOT NULL,
    [FileSize]     INT            CONSTRAINT [DF__MusicEntr__FileSize] DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

