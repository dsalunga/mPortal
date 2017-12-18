IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ContactLink_Mode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ContactLink] DROP CONSTRAINT [DF_ContactLink_Mode]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactLink]') AND type in (N'U'))
DROP TABLE [dbo].[ContactLink]
GO
