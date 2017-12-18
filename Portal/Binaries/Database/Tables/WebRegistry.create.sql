SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebRegistry]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebRegistry](
	[RegistryId] [int] NOT NULL,
	[Key] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Value] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ParentId] [int] NOT NULL,
	[StageId] [int] NOT NULL,
 CONSTRAINT [PK_WebRegistry] PRIMARY KEY CLUSTERED 
(
	[RegistryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebRegistry_StageId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebRegistry] ADD  CONSTRAINT [DF_WebRegistry_StageId]  DEFAULT ((-1)) FOR [StageId]
END

GO
