SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebSubscription]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebSubscription](
	[SubscriptionId] [int] NOT NULL,
	[ObjectId] [int] NOT NULL,
	[RecordId] [int] NOT NULL,
	[PartId] [int] NOT NULL,
	[PageId] [int] NOT NULL,
	[Allow] [int] NOT NULL,
 CONSTRAINT [PK_WebSubscription] PRIMARY KEY CLUSTERED 
(
	[SubscriptionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebSubscription_PartId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSubscription] ADD  CONSTRAINT [DF_WebSubscription_PartId]  DEFAULT ((-1)) FOR [PartId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ArticleSubscription_PageId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSubscription] ADD  CONSTRAINT [DF_ArticleSubscription_PageId]  DEFAULT ((-1)) FOR [PageId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ArticleSubscription_Allow]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSubscription] ADD  CONSTRAINT [DF_ArticleSubscription_Allow]  DEFAULT ((1)) FOR [Allow]
END

GO
