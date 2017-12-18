SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebOffice]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebOffice](
	[OfficeId] [int] NOT NULL,
	[Name] [nvarchar](250) COLLATE Latin1_General_CI_AI NOT NULL,
	[ParentId] [int] NOT NULL,
	[AddressLine1] [nvarchar](250) COLLATE Latin1_General_CI_AI NOT NULL,
	[MobileNumber] [nvarchar](50) COLLATE Latin1_General_CI_AI NOT NULL,
	[PhoneNumber] [nvarchar](50) COLLATE Latin1_General_CI_AI NOT NULL,
	[EmailAddress] [nvarchar](50) COLLATE Latin1_General_CI_AI NOT NULL,
	[ContactPerson] [nvarchar](250) COLLATE Latin1_General_CI_AI NOT NULL,
 CONSTRAINT [PK_WebOffice] PRIMARY KEY CLUSTERED 
(
	[OfficeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebOffice_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebOffice] ADD  CONSTRAINT [DF_WebOffice_Name]  DEFAULT ('') FOR [Name]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebOffice_ParentId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebOffice] ADD  CONSTRAINT [DF_WebOffice_ParentId]  DEFAULT ((-1)) FOR [ParentId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebOffice_AddressLine1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebOffice] ADD  CONSTRAINT [DF_WebOffice_AddressLine1]  DEFAULT ('') FOR [AddressLine1]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebOffice_MobileNumber]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebOffice] ADD  CONSTRAINT [DF_WebOffice_MobileNumber]  DEFAULT ('') FOR [MobileNumber]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebOffice_PhoneNumber]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebOffice] ADD  CONSTRAINT [DF_WebOffice_PhoneNumber]  DEFAULT ('') FOR [PhoneNumber]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebOffice_EmailAddress]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebOffice] ADD  CONSTRAINT [DF_WebOffice_EmailAddress]  DEFAULT ('') FOR [EmailAddress]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebOffice_ContactPersonId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebOffice] ADD  CONSTRAINT [DF_WebOffice_ContactPersonId]  DEFAULT ('') FOR [ContactPerson]
END

GO
