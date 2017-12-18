CREATE TABLE [dbo].[SiteListProperty] (
    [ListingPagePropertyID] INT            IDENTITY (1, 1) NOT NULL,
    [PageType]              INT            NULL,
    [SitePageItemID]        INT            NULL,
    [ParentID]              INT            NULL,
    [RepeatColumns]         INT            NULL,
    [HeaderText]            NVARCHAR (256) NULL,
    [CellPadding]           INT            NULL,
    [SortByName]            BIT            NULL,
    CONSTRAINT [PK_SiteListProperty] PRIMARY KEY CLUSTERED ([ListingPagePropertyID] ASC)
);

