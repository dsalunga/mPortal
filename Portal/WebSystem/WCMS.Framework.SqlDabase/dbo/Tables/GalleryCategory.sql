CREATE TABLE [dbo].[GalleryCategory] (
    [CategoryID]   INT            IDENTITY (1, 1) NOT NULL,
    [Title]        NVARCHAR (256) NULL,
    [ImageURL]     NVARCHAR (256) NULL,
    [Width]        INT            CONSTRAINT [DF_GalleryCategory_Width] DEFAULT ((-1)) NOT NULL,
    [PhotoHeight]  INT            CONSTRAINT [DF_GalleryCategory_PhotoHeight] DEFAULT ((75)) NOT NULL,
    [FolderName]   NVARCHAR (250) COLLATE Latin1_General_CI_AI CONSTRAINT [DF_GalleryCategory_FolderName] DEFAULT ('') NOT NULL,
    [PhotoWidth]   INT            CONSTRAINT [DF_GalleryCategory_PhotoWidth] DEFAULT ((112)) NOT NULL,
    [DateModified] DATETIME       CONSTRAINT [DF_GalleryCategory_DateModified] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_GalleryCategory] PRIMARY KEY CLUSTERED ([CategoryID] ASC)
);

