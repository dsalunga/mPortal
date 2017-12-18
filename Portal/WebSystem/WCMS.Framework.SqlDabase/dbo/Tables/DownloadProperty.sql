CREATE TABLE [dbo].[DownloadProperty] (
    [DownloadPropertyID] INT            IDENTITY (1, 1) NOT NULL,
    [PageType]           INT            NULL,
    [SitePageItemID]     INT            NULL,
    [InitialControl]     NVARCHAR (255) NULL,
    [Columns]            INT            NULL,
    [Rows]               INT            NULL,
    [MaxRecords]         INT            NULL,
    [ForceDownload]      BIT            NULL,
    CONSTRAINT [PK_DownloadProperty] PRIMARY KEY CLUSTERED ([DownloadPropertyID] ASC)
);

