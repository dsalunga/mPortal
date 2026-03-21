CREATE TABLE [dbo].[StdMenu] (
    [StdMenuID]      INT           IDENTITY (1, 1) NOT NULL,
    [SitePageItemID] INT           NULL,
    [PageType]       INT           NULL,
    [Width]          NVARCHAR (64) NULL,
    [Height]         NVARCHAR (64) NULL,
    [Horizontal]     BIT           NULL,
    [SiteID]         INT           NULL,
    [ShowHome]       BIT           NULL,
    [SiteSectionID]  INT           NULL,
    [HomeText]       NVARCHAR (64) NULL,
    CONSTRAINT [PK_StdMenu] PRIMARY KEY CLUSTERED ([StdMenuID] ASC)
);

