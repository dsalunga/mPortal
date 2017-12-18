SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebObjectSecurityPermission]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebObjectSecurityPermission](
	[Id] [int] NOT NULL,
	[ObjectSecurityId] [int] NOT NULL,
	[PermissionId] [int] NOT NULL,
	[Allow] [int] NOT NULL,
	[Deny] [int] NOT NULL,
 CONSTRAINT [PK_WebObjectSecurityPermission] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObjectSecurityPermission_Allow]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObjectSecurityPermission] ADD  CONSTRAINT [DF_WebObjectSecurityPermission_Allow]  DEFAULT ((1)) FOR [Allow]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObjectSecurityPermission_Deny]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObjectSecurityPermission] ADD  CONSTRAINT [DF_WebObjectSecurityPermission_Deny]  DEFAULT ((0)) FOR [Deny]
END

GO
