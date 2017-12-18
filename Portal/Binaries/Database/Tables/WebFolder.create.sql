SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebFolder]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebFolder](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ParentId] [int] NOT NULL,
	[ShareName] [nvarchar](250) COLLATE Latin1_General_CI_AI NOT NULL,
	[ObjectId] [int] NOT NULL,
	[SiteId] [int] NOT NULL,
 CONSTRAINT [PK_WebFolder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebFolder_ShareName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebFolder] ADD  CONSTRAINT [DF_WebFolder_ShareName]  DEFAULT ('') FOR [ShareName]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebFolder_ObjectId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebFolder] ADD  CONSTRAINT [DF_WebFolder_ObjectId]  DEFAULT ((-1)) FOR [ObjectId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebFolder_SiteId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebFolder] ADD  CONSTRAINT [DF_WebFolder_SiteId]  DEFAULT ((-1)) FOR [SiteId]
END

GO
