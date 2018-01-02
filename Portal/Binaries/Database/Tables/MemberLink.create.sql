SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MemberLink]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MemberLink](
	[MemberLinkId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[MemberId] [int] NOT NULL,
	[ExternalIdNo] [nvarchar](50) COLLATE Latin1_General_CI_AI NOT NULL,
	[HomeAddressLine1] [nvarchar](250) COLLATE Latin1_General_CI_AI NOT NULL,
	[HomeAddressStateCode] [int] NOT NULL,
	[HomeAddressCountryCode] [int] NOT NULL,
	[HomeAddressZipCode] [nvarchar](50) COLLATE Latin1_General_CI_AI NOT NULL,
	[MobileNumber] [nvarchar](50) COLLATE Latin1_General_CI_AI NOT NULL,
	[HomePhone] [nvarchar](50) COLLATE Latin1_General_CI_AI NOT NULL,
	[WorkAddressLine1] [nvarchar](250) COLLATE Latin1_General_CI_AI NOT NULL,
	[WorkAddressStateCode] [int] NOT NULL,
	[WorkAddressCountryCode] [int] NOT NULL,
	[WorkAddressZipCode] [nvarchar](50) COLLATE Latin1_General_CI_AI NOT NULL,
	[WorkDesignation] [nvarchar](250) COLLATE Latin1_General_CI_AI NOT NULL,
	[WorkPhone] [nvarchar](50) COLLATE Latin1_General_CI_AI NOT NULL,
	[Nickname] [nvarchar](50) COLLATE Latin1_General_CI_AI NOT NULL,
	[LastUpdate] [datetime] NOT NULL,
	[PhotoPath] [nvarchar](500) COLLATE Latin1_General_CI_AI NOT NULL,
	[MembershipDate] [datetime] NOT NULL,
	[Approved] [int] NOT NULL,
	[Locale] [nvarchar](2000) COLLATE Latin1_General_CI_AI NOT NULL,
	[HomeAddressLine2] [nvarchar](250) COLLATE Latin1_General_CI_AI NOT NULL,
	[WorkAddressLine2] [nvarchar](250) COLLATE Latin1_General_CI_AI NOT NULL,
	[Private] [int] NOT NULL,
	[AdditionalInfo] [nvarchar](4000) COLLATE Latin1_General_CI_AI NOT NULL,
	[LocaleId] [int] NOT NULL,
 CONSTRAINT [PK_MemberLink] PRIMARY KEY CLUSTERED 
(
	[MemberLinkId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_MemberId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] ADD  CONSTRAINT [DF_MemberLink_MemberId]  DEFAULT ((-1)) FOR [MemberId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_ExternalIdNo]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] ADD  CONSTRAINT [DF_MemberLink_ExternalIdNo]  DEFAULT ('') FOR [ExternalIdNo]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_HomeAddressLine1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] ADD  CONSTRAINT [DF_MemberLink_HomeAddressLine1]  DEFAULT ('') FOR [HomeAddressLine1]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_HomeAddressStateCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] ADD  CONSTRAINT [DF_MemberLink_HomeAddressStateCode]  DEFAULT ((-1)) FOR [HomeAddressStateCode]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_HomeAddressCountryCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] ADD  CONSTRAINT [DF_MemberLink_HomeAddressCountryCode]  DEFAULT ((-1)) FOR [HomeAddressCountryCode]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_HomeAddressZipCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] ADD  CONSTRAINT [DF_MemberLink_HomeAddressZipCode]  DEFAULT ('') FOR [HomeAddressZipCode]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_MobileNumber]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] ADD  CONSTRAINT [DF_MemberLink_MobileNumber]  DEFAULT ('') FOR [MobileNumber]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_HomePhone]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] ADD  CONSTRAINT [DF_MemberLink_HomePhone]  DEFAULT ('') FOR [HomePhone]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_WorkAddressLine1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] ADD  CONSTRAINT [DF_MemberLink_WorkAddressLine1]  DEFAULT ('') FOR [WorkAddressLine1]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_WorkAddressStateCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] ADD  CONSTRAINT [DF_MemberLink_WorkAddressStateCode]  DEFAULT ((-1)) FOR [WorkAddressStateCode]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_WorkAddressCountryCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] ADD  CONSTRAINT [DF_MemberLink_WorkAddressCountryCode]  DEFAULT ((-1)) FOR [WorkAddressCountryCode]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_WorkAddressZipCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] ADD  CONSTRAINT [DF_MemberLink_WorkAddressZipCode]  DEFAULT ('') FOR [WorkAddressZipCode]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_WorkDesignation]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] ADD  CONSTRAINT [DF_MemberLink_WorkDesignation]  DEFAULT ('') FOR [WorkDesignation]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_WorkPhone]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] ADD  CONSTRAINT [DF_MemberLink_WorkPhone]  DEFAULT ('') FOR [WorkPhone]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_Nickname]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] ADD  CONSTRAINT [DF_MemberLink_Nickname]  DEFAULT ('') FOR [Nickname]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_LastUpdate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] ADD  CONSTRAINT [DF_MemberLink_LastUpdate]  DEFAULT (getdate()) FOR [LastUpdate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_PhotoPath]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] ADD  CONSTRAINT [DF_MemberLink_PhotoPath]  DEFAULT ('') FOR [PhotoPath]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_MembershipDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] ADD  CONSTRAINT [DF_MemberLink_MembershipDate]  DEFAULT (getdate()) FOR [MembershipDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_Approved]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] ADD  CONSTRAINT [DF_MemberLink_Approved]  DEFAULT ((0)) FOR [Approved]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_Locale]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] ADD  CONSTRAINT [DF_MemberLink_Locale]  DEFAULT ('') FOR [Locale]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_HomeAddressLine11]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] ADD  CONSTRAINT [DF_MemberLink_HomeAddressLine11]  DEFAULT ('') FOR [HomeAddressLine2]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_HomeAddressLine11_1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] ADD  CONSTRAINT [DF_MemberLink_HomeAddressLine11_1]  DEFAULT ('') FOR [WorkAddressLine2]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_Private]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] ADD  CONSTRAINT [DF_MemberLink_Private]  DEFAULT ((0)) FOR [Private]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MemberLink_AdditionalInformation]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] ADD  CONSTRAINT [DF_MemberLink_AdditionalInformation]  DEFAULT ('') FOR [AdditionalInfo]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MemberLin__Local__66A5BBE8]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MemberLink] ADD  DEFAULT ((-1)) FOR [LocaleId]
END

GO
