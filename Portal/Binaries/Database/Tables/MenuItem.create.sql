SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuItem]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MenuItem](
	[Id] [int] NOT NULL,
	[NavigateUrl] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Text] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Target] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ParentId] [int] NOT NULL,
	[MenuId] [int] NOT NULL,
	[IsActive] [int] NOT NULL,
	[Rank] [int] NOT NULL,
	[PageId] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[CheckPermission] [int] NOT NULL,
 CONSTRAINT [PK_MenuItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MenuItem_ParentId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MenuItem] ADD  CONSTRAINT [DF_MenuItem_ParentId]  DEFAULT ((-1)) FOR [ParentId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MenuItem_PageId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MenuItem] ADD  CONSTRAINT [DF_MenuItem_PageId]  DEFAULT ((-1)) FOR [PageId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MenuItem_Type]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MenuItem] ADD  CONSTRAINT [DF_MenuItem_Type]  DEFAULT ((1)) FOR [Type]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MenuItem_CheckPermission]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MenuItem] ADD  CONSTRAINT [DF_MenuItem_CheckPermission]  DEFAULT ((0)) FOR [CheckPermission]
END

GO
