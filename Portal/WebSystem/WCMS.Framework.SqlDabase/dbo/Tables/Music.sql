CREATE TABLE [dbo].[Music] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Title]        NVARCHAR (500) CONSTRAINT [DF__Music__Title] DEFAULT ('') NOT NULL,
    [LanguageCode] NVARCHAR (50)  CONSTRAINT [DF__Music__LanguageCode] DEFAULT ('') NOT NULL,
    [CountryCode]  NVARCHAR (50)  CONSTRAINT [DF__Music__CountryCode] DEFAULT ('') NOT NULL,
    [FolderName]   NVARCHAR (500) CONSTRAINT [DF__Music__FolderName] DEFAULT ('') NOT NULL,
    [Tags]         NVARCHAR (MAX) CONSTRAINT [DF__Music__Tags] DEFAULT ('') NOT NULL,
    [IsOriginal]   INT            CONSTRAINT [DF__Music__IsOriginal] DEFAULT ((1)) NOT NULL,
    [ParentId]     INT            CONSTRAINT [DF__Music__ParentId] DEFAULT ((-1)) NOT NULL,
    [CategoryId]   INT            CONSTRAINT [DF__Music__CategoryId] DEFAULT ((-1)) NOT NULL,
    [DateModified] DATETIME       CONSTRAINT [DF__Music__DateModified] DEFAULT (getdate()) NOT NULL,
    [Composer]     NVARCHAR (500) CONSTRAINT [DF__Music__Composer] DEFAULT ('') NOT NULL,
    [DateComposed] DATETIME       CONSTRAINT [DF__Music__DateComposed] DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

