SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebPartControl]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebPartControl](
	[PartControlId] [int] NOT NULL,
	[PartId] [int] NOT NULL,
	[Name] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Identity] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ConfigFileName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PartAdminId] [int] NOT NULL,
	[EntryPoint] [int] NOT NULL,
	[ParentId] [int] NOT NULL,
 CONSTRAINT [PK_WebPartControls] PRIMARY KEY CLUSTERED 
(
	[PartControlId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPartControl_AdminPartId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPartControl] ADD  CONSTRAINT [DF_WebPartControl_AdminPartId]  DEFAULT ((-1)) FOR [PartAdminId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebPartCo__Entry]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPartControl] ADD  CONSTRAINT [DF__WebPartCo__Entry]  DEFAULT ((1)) FOR [EntryPoint]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPartControl_ParentId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPartControl] ADD  CONSTRAINT [DF_WebPartControl_ParentId]  DEFAULT ((-1)) FOR [ParentId]
END

GO
