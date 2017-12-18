SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebComment]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebComment](
	[Id] [int] NOT NULL,
	[Content] [ntext] COLLATE Latin1_General_CI_AI NOT NULL,
	[UserId] [int] NOT NULL,
	[ObjectId] [int] NOT NULL,
	[RecordId] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[ParentId] [int] NOT NULL,
	[UserName] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UserEmail] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_WebComment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebComment_ParentId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebComment] ADD  CONSTRAINT [DF_WebComment_ParentId]  DEFAULT ((-1)) FOR [ParentId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebCommen__UserName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebComment] ADD  CONSTRAINT [DF__WebCommen__UserName]  DEFAULT ('') FOR [UserName]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebCommen__UserEmail]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebComment] ADD  CONSTRAINT [DF__WebCommen__UserEmail]  DEFAULT ('') FOR [UserEmail]
END

GO
