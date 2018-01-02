SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sportsfest]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Sportsfest](
	[Id] [int] NOT NULL,
	[MemberId] [int] NOT NULL,
	[Name] [nvarchar](100) COLLATE Latin1_General_CI_AI NOT NULL,
	[GroupColor] [nvarchar](50) COLLATE Latin1_General_CI_AI NOT NULL,
	[Age] [int] NOT NULL,
	[Mobile] [nvarchar](50) COLLATE Latin1_General_CI_AI NOT NULL,
	[EntryDate] [datetime] NOT NULL,
	[Sports] [nvarchar](50) COLLATE Latin1_General_CI_AI NOT NULL,
	[Locale] [nvarchar](500) COLLATE Latin1_General_CI_AI NOT NULL,
	[Suggestion] [nvarchar](2500) COLLATE Latin1_General_CI_AI NOT NULL,
	[CountryCode] [int] NOT NULL,
	[ShirtSize] [nvarchar](50) COLLATE Latin1_General_CI_AI NOT NULL,
 CONSTRAINT [PK_Sportsfest] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Sportsfest_MemberId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Sportsfest] ADD  CONSTRAINT [DF_Sportsfest_MemberId]  DEFAULT ((-1)) FOR [MemberId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Sportsfest_EntryDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Sportsfest] ADD  CONSTRAINT [DF_Sportsfest_EntryDate]  DEFAULT (getdate()) FOR [EntryDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Sportsfest_Locale]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Sportsfest] ADD  CONSTRAINT [DF_Sportsfest_Locale]  DEFAULT ('') FOR [Locale]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Sportsfest_Suggestion]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Sportsfest] ADD  CONSTRAINT [DF_Sportsfest_Suggestion]  DEFAULT ('') FOR [Suggestion]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Sportsfest_CountryCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Sportsfest] ADD  CONSTRAINT [DF_Sportsfest_CountryCode]  DEFAULT ((-1)) FOR [CountryCode]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Sportsfest_ShirtSize]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Sportsfest] ADD  CONSTRAINT [DF_Sportsfest_ShirtSize]  DEFAULT ('') FOR [ShirtSize]
END

GO
