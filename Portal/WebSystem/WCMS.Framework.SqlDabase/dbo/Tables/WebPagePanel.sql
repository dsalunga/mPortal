CREATE TABLE [dbo].[WebPagePanel] (
    [PagePanelId]     INT NOT NULL,
    [TemplatePanelId] INT NOT NULL,
    [PageId]          INT CONSTRAINT [DF_WebPagePanels_PageId] DEFAULT ((-1)) NOT NULL,
    [UsageTypeId]     INT CONSTRAINT [DF_WebPagePanels_UsageTypeId] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_WebPagePanels] PRIMARY KEY CLUSTERED ([PagePanelId] ASC)
);

