SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactInquiry]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ContactInquiry](
	[InquiryId] [int] NOT NULL,
	[SenderName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Subject] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Message] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Email] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Address1] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Address2] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[City] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CountryCode] [int] NULL,
	[StateCode] [int] NULL,
	[ZipCode] [nvarchar](63) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Phone] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Fax] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SendTo] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[InqDateTime] [datetime] NULL,
	[IsActive] [int] NULL,
	[SendToEmail] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[InquiryType] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RecordId] [int] NULL,
	[ObjectId] [int] NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Inquiries] PRIMARY KEY CLUSTERED 
(
	[InquiryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ContactInquiry_UserId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ContactInquiry] ADD  CONSTRAINT [DF_ContactInquiry_UserId]  DEFAULT ((-1)) FOR [UserId]
END

GO
