SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Registration]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Registration](
	[Id] [int] NOT NULL,
	[EntryDate] [datetime] NOT NULL,
	[Country] [nvarchar](250) COLLATE Latin1_General_CI_AI NOT NULL,
	[Locale] [nvarchar](500) COLLATE Latin1_General_CI_AI NOT NULL,
	[ExternalId] [nvarchar](50) COLLATE Latin1_General_CI_AI NOT NULL,
	[Name] [nvarchar](100) COLLATE Latin1_General_CI_AI NOT NULL,
	[Designation] [nvarchar](250) COLLATE Latin1_General_CI_AI NOT NULL,
	[ArrivalDate] [datetime] NOT NULL,
	[Airline] [nvarchar](250) COLLATE Latin1_General_CI_AI NOT NULL,
	[FlightNo] [nvarchar](250) COLLATE Latin1_General_CI_AI NOT NULL,
	[DepartureDate] [datetime] NOT NULL,
	[Address] [nvarchar](2500) COLLATE Latin1_General_CI_AI NOT NULL,
	[Age] [int] NOT NULL,
	[Gender] [nvarchar](50) COLLATE Latin1_General_CI_AI NOT NULL,
	[PlaceType] [nvarchar](250) COLLATE Latin1_General_CI_AI NOT NULL,
 CONSTRAINT [PK_Registration] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Registration_EntryDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Registration] ADD  CONSTRAINT [DF_Registration_EntryDate]  DEFAULT (getdate()) FOR [EntryDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Registration_Country]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Registration] ADD  CONSTRAINT [DF_Registration_Country]  DEFAULT ('') FOR [Country]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Registration_Locale]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Registration] ADD  CONSTRAINT [DF_Registration_Locale]  DEFAULT ('') FOR [Locale]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Registration_ExternalId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Registration] ADD  CONSTRAINT [DF_Registration_ExternalId]  DEFAULT ('') FOR [ExternalId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Registration_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Registration] ADD  CONSTRAINT [DF_Registration_Name]  DEFAULT ('') FOR [Name]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Registration_Designation]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Registration] ADD  CONSTRAINT [DF_Registration_Designation]  DEFAULT ('') FOR [Designation]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Registration_Airline]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Registration] ADD  CONSTRAINT [DF_Registration_Airline]  DEFAULT ('') FOR [Airline]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Registration_FlightNo]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Registration] ADD  CONSTRAINT [DF_Registration_FlightNo]  DEFAULT ('') FOR [FlightNo]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Registration_Suggestion]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Registration] ADD  CONSTRAINT [DF_Registration_Suggestion]  DEFAULT ('') FOR [Address]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Registration_Age]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Registration] ADD  CONSTRAINT [DF_Registration_Age]  DEFAULT ((-1)) FOR [Age]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Registration_Gender]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Registration] ADD  CONSTRAINT [DF_Registration_Gender]  DEFAULT ('') FOR [Gender]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Registration_PlaceType]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Registration] ADD  CONSTRAINT [DF_Registration_PlaceType]  DEFAULT ('') FOR [PlaceType]
END

GO
