SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WallUpdate]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WallUpdate](
	[Id] [int] NOT NULL,
	[UpdateRecordId] [int] NOT NULL,
	[UpdateObjectId] [int] NOT NULL,
	[ByRecordId] [int] NOT NULL,
	[ByObjectId] [int] NOT NULL,
	[Content] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[EventTypeId] [int] NOT NULL,
 CONSTRAINT [PK_WallUpdate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Wall_Content]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WallUpdate] ADD  CONSTRAINT [DF_Wall_Content]  DEFAULT ('') FOR [Content]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Wall_UpdateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WallUpdate] ADD  CONSTRAINT [DF_Wall_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Wall_EventTypeId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WallUpdate] ADD  CONSTRAINT [DF_Wall_EventTypeId]  DEFAULT ((-1)) FOR [EventTypeId]
END

GO
