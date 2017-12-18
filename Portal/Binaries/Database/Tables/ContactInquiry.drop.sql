IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ContactInquiry_UserId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ContactInquiry] DROP CONSTRAINT [DF_ContactInquiry_UserId]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactInquiry]') AND type in (N'U'))
DROP TABLE [dbo].[ContactInquiry]
GO
