SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebGroup]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebGroup](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ParentId] [int] NOT NULL,
	[IsSystem] [int] NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[OwnerId] [int] NOT NULL,
	[JoinApproval] [int] NOT NULL,
	[JoinAlert] [int] NOT NULL,
	[PageUrl] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PageId] [int] NOT NULL,
	[Description] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Managers] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_WebGroups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebGroup_ParentId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebGroup] ADD  CONSTRAINT [DF_WebGroup_ParentId]  DEFAULT ((-1)) FOR [ParentId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebGroup_IsSystem]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebGroup] ADD  CONSTRAINT [DF_WebGroup_IsSystem]  DEFAULT ((0)) FOR [IsSystem]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebGroup_DateModified]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebGroup] ADD  CONSTRAINT [DF_WebGroup_DateModified]  DEFAULT (getdate()) FOR [DateModified]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebGroup_OwnerId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebGroup] ADD  CONSTRAINT [DF_WebGroup_OwnerId]  DEFAULT ((-1)) FOR [OwnerId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebGroup_JoinApproval]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebGroup] ADD  CONSTRAINT [DF_WebGroup_JoinApproval]  DEFAULT ((0)) FOR [JoinApproval]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebGroup_JoinAlert]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebGroup] ADD  CONSTRAINT [DF_WebGroup_JoinAlert]  DEFAULT ((0)) FOR [JoinAlert]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebGroup_PageUrl]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebGroup] ADD  CONSTRAINT [DF_WebGroup_PageUrl]  DEFAULT ('') FOR [PageUrl]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebGroup_PageId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebGroup] ADD  CONSTRAINT [DF_WebGroup_PageId]  DEFAULT ((-1)) FOR [PageId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebGroup_Description]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebGroup] ADD  CONSTRAINT [DF_WebGroup_Description]  DEFAULT ('') FOR [Description]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebGroup__Managers]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebGroup] ADD  CONSTRAINT [DF__WebGroup__Managers]  DEFAULT ('') FOR [Managers]
END

GO
