SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Menu]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Menu](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[IsActive] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[SiteId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[PageId] [int] NOT NULL,
	[IncludeChildren] [int] NOT NULL,
 CONSTRAINT [PK_Menus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Menu_RefPageId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Menu] ADD  CONSTRAINT [DF_Menu_RefPageId]  DEFAULT ((-2)) FOR [PageId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Menu_IncludeChildren]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Menu] ADD  CONSTRAINT [DF_Menu_IncludeChildren]  DEFAULT ((0)) FOR [IncludeChildren]
END

GO
