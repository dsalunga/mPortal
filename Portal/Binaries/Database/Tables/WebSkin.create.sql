SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebSkin]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebSkin](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Rank] [int] NOT NULL,
	[ObjectId] [int] NOT NULL,
	[RecordId] [int] NOT NULL,
 CONSTRAINT [PK_WebSkin] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebSkin__Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSkin] ADD  CONSTRAINT [DF__WebSkin__Name]  DEFAULT ('') FOR [Name]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebSkin__Rank]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSkin] ADD  CONSTRAINT [DF__WebSkin__Rank]  DEFAULT ((0)) FOR [Rank]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebSkin__ObjectId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSkin] ADD  CONSTRAINT [DF__WebSkin__ObjectId]  DEFAULT ((-1)) FOR [ObjectId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebSkin__RecordId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebSkin] ADD  CONSTRAINT [DF__WebSkin__RecordId]  DEFAULT ((-1)) FOR [RecordId]
END

GO
