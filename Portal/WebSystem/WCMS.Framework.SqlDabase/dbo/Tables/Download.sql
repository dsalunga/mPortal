CREATE TABLE [dbo].[Download] (
    [DownloadID]   INT              IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (255)   NULL,
    [Description]  NVARCHAR (MAX)   NULL,
    [FileDate]     DATETIME         NULL,
    [Filename]     NVARCHAR (255)   NULL,
    [DateModified] DATETIME         NULL,
    [Rank]         INT              NULL,
    [UserId]       UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Download] PRIMARY KEY CLUSTERED ([DownloadID] ASC)
);

