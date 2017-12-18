SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactLink]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ContactLink](
	[Id] [int] NOT NULL,
	[RecordId] [int] NOT NULL,
	[ObjectId] [int] NOT NULL,
	[ContactId] [int] NOT NULL,
	[Mode] [int] NOT NULL,
 CONSTRAINT [PK_SiteProperties_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ContactLink_Mode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ContactLink] ADD  CONSTRAINT [DF_ContactLink_Mode]  DEFAULT ((0)) FOR [Mode]
END

GO
