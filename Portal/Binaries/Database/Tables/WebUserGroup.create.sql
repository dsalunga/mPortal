SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebUserGroup]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebUserGroup](
	[Id] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[GroupId] [int] NOT NULL,
	[Active] [int] NOT NULL,
	[DateJoined] [datetime] NOT NULL,
	[ObjectId] [int] NOT NULL,
	[RecordId] [int] NOT NULL,
	[Remarks] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CreatedById] [int] NOT NULL,
 CONSTRAINT [PK_WebUserRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUserGroup_Active]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUserGroup] ADD  CONSTRAINT [DF_WebUserGroup_Active]  DEFAULT ((1)) FOR [Active]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUserGroup_DateJoined]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUserGroup] ADD  CONSTRAINT [DF_WebUserGroup_DateJoined]  DEFAULT (getdate()) FOR [DateJoined]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUserGroup_ObjectId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUserGroup] ADD  CONSTRAINT [DF_WebUserGroup_ObjectId]  DEFAULT ((21)) FOR [ObjectId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUserGroup_RecordId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUserGroup] ADD  CONSTRAINT [DF_WebUserGroup_RecordId]  DEFAULT ((-1)) FOR [RecordId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUserGroup_Remarks]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUserGroup] ADD  CONSTRAINT [DF_WebUserGroup_Remarks]  DEFAULT ('') FOR [Remarks]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUserGroup_CreatedById]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUserGroup] ADD  CONSTRAINT [DF_WebUserGroup_CreatedById]  DEFAULT ((-1)) FOR [CreatedById]
END

GO
