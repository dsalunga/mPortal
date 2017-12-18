SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Country]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Country](
	[CountryCode] [int] NOT NULL,
	[CountryName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RegionCode] [int] NOT NULL,
	[Description] [nvarchar](500) COLLATE Latin1_General_CI_AI NOT NULL,
	[ISOCode] [nvarchar](50) COLLATE Latin1_General_CI_AI NOT NULL,
	[DialingCode] [int] NOT NULL,
	[MaxPhoneDigit] [int] NOT NULL,
	[ISOCode3] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ShortName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ISONumeric] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_MISC_Countries] PRIMARY KEY CLUSTERED 
(
	[CountryCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Country_CountryName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Country] ADD  CONSTRAINT [DF_Country_CountryName]  DEFAULT ('') FOR [CountryName]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Country_RegionCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Country] ADD  CONSTRAINT [DF_Country_RegionCode]  DEFAULT ((-1)) FOR [RegionCode]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Country_Description]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Country] ADD  CONSTRAINT [DF_Country_Description]  DEFAULT ('') FOR [Description]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Country_ISOCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Country] ADD  CONSTRAINT [DF_Country_ISOCode]  DEFAULT ('') FOR [ISOCode]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Country_DialingCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Country] ADD  CONSTRAINT [DF_Country_DialingCode]  DEFAULT ((-1)) FOR [DialingCode]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Country_MaxPhoneDigit]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Country] ADD  CONSTRAINT [DF_Country_MaxPhoneDigit]  DEFAULT ((-1)) FOR [MaxPhoneDigit]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Country__ISOCode3]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Country] ADD  CONSTRAINT [DF__Country__ISOCode3]  DEFAULT ('') FOR [ISOCode3]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Country__ShortName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Country] ADD  CONSTRAINT [DF__Country__ShortName]  DEFAULT ('') FOR [ShortName]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Country__ISONumeric]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Country] ADD  CONSTRAINT [DF__Country__ISONumeric]  DEFAULT ('') FOR [ISONumeric]
END

GO
