SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MCComposer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MCComposer](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](500) COLLATE Latin1_General_CI_AI NOT NULL,
	[Entry] [nvarchar](max) COLLATE Latin1_General_CI_AI NOT NULL,
	[Locale] [nvarchar](500) COLLATE Latin1_General_CI_AI NOT NULL,
	[Work] [nvarchar](500) COLLATE Latin1_General_CI_AI NOT NULL,
	[Description] [nvarchar](max) COLLATE Latin1_General_CI_AI NOT NULL,
	[PhotoFile] [nvarchar](500) COLLATE Latin1_General_CI_AI NOT NULL,
	[NickName] [nvarchar](500) COLLATE Latin1_General_CI_AI NOT NULL,
	[Active] [int] NOT NULL,
	[CompetitionId] [int] NOT NULL,
 CONSTRAINT [PK__MCComposer_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCComposer__Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCComposer] ADD  CONSTRAINT [DF__MCComposer__Name]  DEFAULT ('') FOR [Name]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCComposer__Entry]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCComposer] ADD  CONSTRAINT [DF__MCComposer__Entry]  DEFAULT ('') FOR [Entry]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCComposer__Locale]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCComposer] ADD  CONSTRAINT [DF__MCComposer__Locale]  DEFAULT ('') FOR [Locale]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCComposer__Work]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCComposer] ADD  CONSTRAINT [DF__MCComposer__Work]  DEFAULT ('') FOR [Work]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCComposer__Description]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCComposer] ADD  CONSTRAINT [DF__MCComposer__Description]  DEFAULT ('') FOR [Description]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCComposer__PhotoFile]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCComposer] ADD  CONSTRAINT [DF__MCComposer__PhotoFile]  DEFAULT ('') FOR [PhotoFile]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCComposer__NickName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCComposer] ADD  CONSTRAINT [DF__MCComposer__NickName]  DEFAULT ('') FOR [NickName]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCComposer__Active]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCComposer] ADD  CONSTRAINT [DF__MCComposer__Active]  DEFAULT ((1)) FOR [Active]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCComposer__CompetitionId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCComposer] ADD  CONSTRAINT [DF__MCComposer__CompetitionId]  DEFAULT ((-1)) FOR [CompetitionId]
END

GO
