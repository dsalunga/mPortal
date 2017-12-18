SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MenuObject]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MenuObject](
	[Id] [int] NOT NULL,
	[RecordId] [int] NOT NULL,
	[ObjectId] [int] NOT NULL,
	[Width] [nvarchar](63) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Height] [nvarchar](63) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Horizontal] [int] NOT NULL,
	[MenuId] [int] NOT NULL,
	[ParameterSetId] [int] NOT NULL,
	[RenderMode] [int] NOT NULL,
 CONSTRAINT [PK_SiteMenu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MenuObject_ParameterSetId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MenuObject] ADD  CONSTRAINT [DF_MenuObject_ParameterSetId]  DEFAULT ((-1)) FOR [ParameterSetId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MenuObject_RenderMode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MenuObject] ADD  CONSTRAINT [DF_MenuObject_RenderMode]  DEFAULT ((0)) FOR [RenderMode]
END

GO
