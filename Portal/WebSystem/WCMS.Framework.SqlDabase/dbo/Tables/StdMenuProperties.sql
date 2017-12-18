CREATE TABLE [dbo].[StdMenuProperties] (
    [ListingPagePropertyID] INT            IDENTITY (1, 1) NOT NULL,
    [PageType]              INT            NULL,
    [SitePageItemID]        INT            NULL,
    [RepeatColumns]         INT            NULL,
    [HeaderText]            NVARCHAR (256) NULL,
    [ParentID]              INT            NULL,
    [ListingType]           NVARCHAR (64)  NULL,
    [CellPadding]           INT            NULL,
    CONSTRAINT [PK_ListingPageProperties] PRIMARY KEY CLUSTERED ([ListingPagePropertyID] ASC)
);

