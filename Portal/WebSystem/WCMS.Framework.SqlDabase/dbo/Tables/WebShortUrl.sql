CREATE TABLE [dbo].[WebShortUrl] (
    [Id]     INT            IDENTITY (1, 1) NOT NULL,
    [Name]   NVARCHAR (500) CONSTRAINT [DF_WebShortUrl_Name] DEFAULT ('') NOT NULL,
    [PageId] INT            CONSTRAINT [DF_WebShortUrl_PageId] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_WebShortUrl] PRIMARY KEY CLUSTERED ([Id] ASC)
);

