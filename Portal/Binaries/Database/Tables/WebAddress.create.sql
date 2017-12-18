SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebAddress]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebAddress](
	[Id] [int] NOT NULL,
	[AddressLine1] [nvarchar](250) COLLATE Latin1_General_CI_AI NOT NULL,
	[AddressLine2] [nvarchar](250) COLLATE Latin1_General_CI_AI NOT NULL,
	[CityTown] [nvarchar](50) COLLATE Latin1_General_CI_AI NOT NULL,
	[StateProvince] [nvarchar](50) COLLATE Latin1_General_CI_AI NOT NULL,
	[StateProvinceCode] [int] NOT NULL,
	[CountryCode] [int] NOT NULL,
	[ZipCode] [nvarchar](50) COLLATE Latin1_General_CI_AI NOT NULL,
	[PhoneNumber] [nvarchar](50) COLLATE Latin1_General_CI_AI NOT NULL,
	[ObjectId] [int] NOT NULL,
	[RecordId] [int] NOT NULL,
	[Tag] [nvarchar](50) COLLATE Latin1_General_CI_AI NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
 CONSTRAINT [PK_WebAddress] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table1_HomeAddressLine1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebAddress] ADD  CONSTRAINT [DF_Table1_HomeAddressLine1]  DEFAULT ('') FOR [AddressLine1]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebAddress_AddressLine2]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebAddress] ADD  CONSTRAINT [DF_WebAddress_AddressLine2]  DEFAULT ('') FOR [AddressLine2]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebAddress_CityTown]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebAddress] ADD  CONSTRAINT [DF_WebAddress_CityTown]  DEFAULT ('') FOR [CityTown]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebAddress_StateProvince]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebAddress] ADD  CONSTRAINT [DF_WebAddress_StateProvince]  DEFAULT ('') FOR [StateProvince]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table1_HomeAddressStateCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebAddress] ADD  CONSTRAINT [DF_Table1_HomeAddressStateCode]  DEFAULT ((-1)) FOR [StateProvinceCode]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table1_HomeAddressCountryCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebAddress] ADD  CONSTRAINT [DF_Table1_HomeAddressCountryCode]  DEFAULT ((-1)) FOR [CountryCode]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table1_HomeAddressZipCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebAddress] ADD  CONSTRAINT [DF_Table1_HomeAddressZipCode]  DEFAULT ('') FOR [ZipCode]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebAddress_PhoneNumber]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebAddress] ADD  CONSTRAINT [DF_WebAddress_PhoneNumber]  DEFAULT ('') FOR [PhoneNumber]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebAddress_UserId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebAddress] ADD  CONSTRAINT [DF_WebAddress_UserId]  DEFAULT ((-1)) FOR [ObjectId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebAddress_RecordId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebAddress] ADD  CONSTRAINT [DF_WebAddress_RecordId]  DEFAULT ((-1)) FOR [RecordId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebAddress_LastUpdated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebAddress] ADD  CONSTRAINT [DF_WebAddress_LastUpdated]  DEFAULT (getdate()) FOR [LastUpdated]
END

GO
