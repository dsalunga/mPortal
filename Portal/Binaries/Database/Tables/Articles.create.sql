SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Articles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Articles](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Image] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Description] [nvarchar](1024) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Date] [datetime] NOT NULL,
	[Content] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Author] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SiteId] [int] NOT NULL,
	[Active] [int] NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[UserId] [int] NOT NULL,
	[ModifiedUserId] [int] NOT NULL,
	[DirectoryId] [int] NOT NULL,
	[Tags] [nvarchar](250) COLLATE Latin1_General_CI_AI NOT NULL,
 CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Articles_DirectoryId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Articles] ADD  CONSTRAINT [DF_Articles_DirectoryId]  DEFAULT ((-1)) FOR [DirectoryId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Articles_Tags]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Articles] ADD  CONSTRAINT [DF_Articles_Tags]  DEFAULT ('') FOR [Tags]
END

GO
