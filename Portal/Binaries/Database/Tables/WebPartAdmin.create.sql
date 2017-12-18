SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebPartAdmin]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebPartAdmin](
	[PartAdminId] [int] NOT NULL,
	[PartId] [int] NOT NULL,
	[Name] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FileName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ParentId] [int] NOT NULL,
	[Active] [int] NOT NULL,
	[Visible] [int] NOT NULL,
	[InSiteContext] [int] NOT NULL,
	[TemplateEngineId] [int] NOT NULL,
	[AutoTitle] [int] NOT NULL,
 CONSTRAINT [PK_WebPartAdmin] PRIMARY KEY CLUSTERED 
(
	[PartAdminId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPartAdmin_Active]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPartAdmin] ADD  CONSTRAINT [DF_WebPartAdmin_Active]  DEFAULT ((1)) FOR [Active]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPartAdmin_Visible]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPartAdmin] ADD  CONSTRAINT [DF_WebPartAdmin_Visible]  DEFAULT ((1)) FOR [Visible]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPartAdmin_InSite]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPartAdmin] ADD  CONSTRAINT [DF_WebPartAdmin_InSite]  DEFAULT ((0)) FOR [InSiteContext]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPartAdmin_EngineId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPartAdmin] ADD  CONSTRAINT [DF_WebPartAdmin_EngineId]  DEFAULT ((1)) FOR [TemplateEngineId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebPartAd__AutoTitle]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPartAdmin] ADD  CONSTRAINT [DF__WebPartAd__AutoTitle]  DEFAULT ((1)) FOR [AutoTitle]
END

GO
