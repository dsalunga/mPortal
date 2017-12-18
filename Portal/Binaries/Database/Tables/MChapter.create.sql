SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MChapter]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MChapter](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](500) COLLATE Latin1_General_CI_AI NOT NULL,
	[ParentId] [int] NOT NULL,
	[Address] [nvarchar](1500) COLLATE Latin1_General_CI_AI NOT NULL,
	[ChapterType] [int] NOT NULL,
	[Latitude] [float] NOT NULL,
	[Longitude] [float] NOT NULL,
	[AccessType] [int] NOT NULL,
	[CountryCode] [int] NOT NULL,
	[Email] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Mobile] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Telephone] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ServiceSchedule] [nvarchar](1500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[MoreInfo] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DistrictNo] [int] NOT NULL,
	[DivisionId] [int] NOT NULL,
	[LastUpdate] [datetime] NOT NULL,
	[Extra] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[LocaleId] [int] NOT NULL,
 CONSTRAINT [PK_MChapter] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MChapter_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] ADD  CONSTRAINT [DF_MChapter_Name]  DEFAULT ('') FOR [Name]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MChapter_ParentId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] ADD  CONSTRAINT [DF_MChapter_ParentId]  DEFAULT ((-1)) FOR [ParentId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebOffice_Address]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] ADD  CONSTRAINT [DF_WebOffice_Address]  DEFAULT ('') FOR [Address]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MChapter_ChapterType]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] ADD  CONSTRAINT [DF_MChapter_ChapterType]  DEFAULT ((0)) FOR [ChapterType]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MChapter_Latitude]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] ADD  CONSTRAINT [DF_MChapter_Latitude]  DEFAULT ((0)) FOR [Latitude]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MChapter_Longitude]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] ADD  CONSTRAINT [DF_MChapter_Longitude]  DEFAULT ((0)) FOR [Longitude]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MChapter_AccessType]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] ADD  CONSTRAINT [DF_MChapter_AccessType]  DEFAULT ((0)) FOR [AccessType]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MChapter_CountryCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] ADD  CONSTRAINT [DF_MChapter_CountryCode]  DEFAULT ((0)) FOR [CountryCode]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MChapter_Email]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] ADD  CONSTRAINT [DF_MChapter_Email]  DEFAULT ('') FOR [Email]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MChapter_Mobile]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] ADD  CONSTRAINT [DF_MChapter_Mobile]  DEFAULT ('') FOR [Mobile]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MChapter_Telephone]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] ADD  CONSTRAINT [DF_MChapter_Telephone]  DEFAULT ('') FOR [Telephone]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MChapter_ServiceSchedule]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] ADD  CONSTRAINT [DF_MChapter_ServiceSchedule]  DEFAULT ('') FOR [ServiceSchedule]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MChapter_MoreInfo]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] ADD  CONSTRAINT [DF_MChapter_MoreInfo]  DEFAULT ('') FOR [MoreInfo]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MChapter_DistrictNo]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] ADD  CONSTRAINT [DF_MChapter_DistrictNo]  DEFAULT ((-1)) FOR [DistrictNo]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MChapter_DivisionId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] ADD  CONSTRAINT [DF_MChapter_DivisionId]  DEFAULT ((-1)) FOR [DivisionId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MChapter_LastUpdate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] ADD  CONSTRAINT [DF_MChapter_LastUpdate]  DEFAULT (getdate()) FOR [LastUpdate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MChapter__Extra__3789439E]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] ADD  CONSTRAINT [DF__MChapter__Extra__3789439E]  DEFAULT ('') FOR [Extra]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MChapter__Locale__387D67D7]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MChapter] ADD  CONSTRAINT [DF__MChapter__Locale__387D67D7]  DEFAULT ((-1)) FOR [LocaleId]
END

GO
