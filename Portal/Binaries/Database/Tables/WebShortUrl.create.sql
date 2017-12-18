SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebShortUrl]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebShortUrl](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PageId] [int] NOT NULL,
	[PageUrl] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_WebShortUrl] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebShortUrl_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebShortUrl] ADD  CONSTRAINT [DF_WebShortUrl_Name]  DEFAULT ('') FOR [Name]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebShortUrl_PageId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebShortUrl] ADD  CONSTRAINT [DF_WebShortUrl_PageId]  DEFAULT ((-1)) FOR [PageId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebShortU_PageUrl]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebShortUrl] ADD  CONSTRAINT [DF_WebShortU_PageUrl]  DEFAULT ('') FOR [PageUrl]
END

GO
