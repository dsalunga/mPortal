SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebRole]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebRole](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[IsSystem] [int] NOT NULL,
	[ParentId] [int] NOT NULL,
 CONSTRAINT [PK_WebRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebRoles_IsSystem]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebRole] ADD  CONSTRAINT [DF_WebRoles_IsSystem]  DEFAULT ((0)) FOR [IsSystem]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebRoles_ParentId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebRole] ADD  CONSTRAINT [DF_WebRoles_ParentId]  DEFAULT ((-1)) FOR [ParentId]
END

GO
