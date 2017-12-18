SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Music]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Music](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[LanguageCode] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CountryCode] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FolderName] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Tags] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[IsOriginal] [int] NOT NULL,
	[ParentId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[Composer] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DateComposed] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Music__Title]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Music] ADD  CONSTRAINT [DF__Music__Title]  DEFAULT ('') FOR [Title]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Music__LanguageCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Music] ADD  CONSTRAINT [DF__Music__LanguageCode]  DEFAULT ('') FOR [LanguageCode]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Music__CountryCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Music] ADD  CONSTRAINT [DF__Music__CountryCode]  DEFAULT ('') FOR [CountryCode]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Music__FolderName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Music] ADD  CONSTRAINT [DF__Music__FolderName]  DEFAULT ('') FOR [FolderName]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Music__Tags]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Music] ADD  CONSTRAINT [DF__Music__Tags]  DEFAULT ('') FOR [Tags]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Music__IsOriginal]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Music] ADD  CONSTRAINT [DF__Music__IsOriginal]  DEFAULT ((1)) FOR [IsOriginal]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Music__ParentId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Music] ADD  CONSTRAINT [DF__Music__ParentId]  DEFAULT ((-1)) FOR [ParentId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Music__CategoryId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Music] ADD  CONSTRAINT [DF__Music__CategoryId]  DEFAULT ((-1)) FOR [CategoryId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Music__DateModified]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Music] ADD  CONSTRAINT [DF__Music__DateModified]  DEFAULT (getdate()) FOR [DateModified]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Music__Composer]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Music] ADD  CONSTRAINT [DF__Music__Composer]  DEFAULT ('') FOR [Composer]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Music__DateComposed]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Music] ADD  CONSTRAINT [DF__Music__DateComposed]  DEFAULT (getdate()) FOR [DateComposed]
END

GO
