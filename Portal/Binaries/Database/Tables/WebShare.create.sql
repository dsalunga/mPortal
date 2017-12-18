SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebShare]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebShare](
	[Id] [int] NOT NULL,
	[ObjectId] [int] NOT NULL,
	[RecordId] [int] NOT NULL,
	[ShareObjectId] [int] NOT NULL,
	[ShareRecordId] [int] NOT NULL,
	[Allow] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebShare__Object]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebShare] ADD  CONSTRAINT [DF__WebShare__Object]  DEFAULT ((-1)) FOR [ObjectId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebShare__Record]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebShare] ADD  CONSTRAINT [DF__WebShare__Record]  DEFAULT ((-1)) FOR [RecordId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebShare__ShareO]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebShare] ADD  CONSTRAINT [DF__WebShare__ShareO]  DEFAULT ((-1)) FOR [ShareObjectId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebShare__ShareR]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebShare] ADD  CONSTRAINT [DF__WebShare__ShareR]  DEFAULT ((-1)) FOR [ShareRecordId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebShare__Allow]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebShare] ADD  CONSTRAINT [DF__WebShare__Allow]  DEFAULT ((1)) FOR [Allow]
END

GO
