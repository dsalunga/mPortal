SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebPermissionSet]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebPermissionSet](
	[Id] [int] NOT NULL,
	[ObjectId] [int] NOT NULL,
	[RecordId] [int] NOT NULL,
	[PermissionId] [int] NOT NULL,
	[Public] [int] NOT NULL,
 CONSTRAINT [PK_WebPermissionSet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPermissionSet_RecordId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPermissionSet] ADD  CONSTRAINT [DF_WebPermissionSet_RecordId]  DEFAULT ((-1)) FOR [RecordId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPermissionSet_Public]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPermissionSet] ADD  CONSTRAINT [DF_WebPermissionSet_Public]  DEFAULT ((0)) FOR [Public]
END

GO
