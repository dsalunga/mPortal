CREATE TABLE [dbo].[BasicListItem] (
    [ListItemID]     INT            IDENTITY (1, 1) NOT NULL,
    [PageType]       INT            NULL,
    [SitePageItemID] INT            NULL,
    [Field1]         NVARCHAR (255) COLLATE Latin1_General_CI_AI NULL,
    [Field2]         NVARCHAR (255) COLLATE Latin1_General_CI_AI NULL,
    [Field3]         NVARCHAR (255) COLLATE Latin1_General_CI_AI NULL,
    [Rank]           INT            NULL,
    CONSTRAINT [PK_ListItems] PRIMARY KEY CLUSTERED ([ListItemID] ASC)
);

