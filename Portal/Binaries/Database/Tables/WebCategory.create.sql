SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebCategory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebCategory](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ObjectId] [int] NOT NULL,
	[Rank] [int] NOT NULL,
	[ParentId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebCategor__Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebCategory] ADD  CONSTRAINT [DF__WebCategor__Name]  DEFAULT ((-1)) FOR [Name]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebCatego__ObjectId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebCategory] ADD  CONSTRAINT [DF__WebCatego__ObjectId]  DEFAULT ((-1)) FOR [ObjectId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebCategor__Rank]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebCategory] ADD  CONSTRAINT [DF__WebCategor__Rank]  DEFAULT ((0)) FOR [Rank]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebCatego__ParentId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebCategory] ADD  CONSTRAINT [DF__WebCatego__ParentId]  DEFAULT ((-1)) FOR [ParentId]
END

GO
