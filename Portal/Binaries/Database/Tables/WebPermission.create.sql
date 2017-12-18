SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebPermission]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebPermission](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[IsSystem] [int] NOT NULL,
 CONSTRAINT [PK_WebPermissions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPermission_IsSystem]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPermission] ADD  CONSTRAINT [DF_WebPermission_IsSystem]  DEFAULT ((0)) FOR [IsSystem]
END

GO
