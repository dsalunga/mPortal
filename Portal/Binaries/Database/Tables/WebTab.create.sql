SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebTab]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebTab](
	[Id] [int] NOT NULL,
	[Text] [nvarchar](250) COLLATE Latin1_General_CI_AI NOT NULL,
	[Target] [nvarchar](250) COLLATE Latin1_General_CI_AI NOT NULL,
	[Rank] [int] NOT NULL,
	[TabSetId] [int] NOT NULL,
	[Name] [nvarchar](250) COLLATE Latin1_General_CI_AI NOT NULL,
 CONSTRAINT [PK_WebTab] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTab_Text]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTab] ADD  CONSTRAINT [DF_WebTab_Text]  DEFAULT ('') FOR [Text]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTab_Target]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTab] ADD  CONSTRAINT [DF_WebTab_Target]  DEFAULT ('') FOR [Target]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTab_Rank]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTab] ADD  CONSTRAINT [DF_WebTab_Rank]  DEFAULT ((0)) FOR [Rank]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTab_TabSetId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTab] ADD  CONSTRAINT [DF_WebTab_TabSetId]  DEFAULT ((-1)) FOR [TabSetId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTab_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTab] ADD  CONSTRAINT [DF_WebTab_Name]  DEFAULT ('') FOR [Name]
END

GO
