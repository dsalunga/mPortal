CREATE TABLE [dbo].[DownloadLocation] (
    [DownloadLocationID] INT IDENTITY (1, 1) NOT NULL,
    [SiteID]             INT NULL,
    [PageType]           INT NULL,
    [SitePageItemID]     INT NULL,
    [DownloadID]         INT NULL,
    CONSTRAINT [PK_DownloadLocation_1] PRIMARY KEY CLUSTERED ([DownloadLocationID] ASC)
);

