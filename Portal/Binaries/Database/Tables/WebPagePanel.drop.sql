IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPagePanels_PageId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPagePanel] DROP CONSTRAINT [DF_WebPagePanels_PageId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPagePanels_UsageTypeId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPagePanel] DROP CONSTRAINT [DF_WebPagePanels_UsageTypeId]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebPagePanel]') AND type in (N'U'))
DROP TABLE [dbo].[WebPagePanel]
GO
