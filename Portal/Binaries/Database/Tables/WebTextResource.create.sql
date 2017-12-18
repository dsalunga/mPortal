SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebTextResource]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebTextResource](
	[TextResourceId] [int] NOT NULL,
	[ContentTypeId] [int] NOT NULL,
	[Title] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Content] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DirectoryId] [int] NOT NULL,
	[Rank] [int] NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[OwnerObjectId] [int] NOT NULL,
	[OwnerRecordId] [int] NOT NULL,
	[DatePersisted] [datetime] NOT NULL,
	[PhysicalPath] [nvarchar](500) COLLATE Latin1_General_CI_AI NOT NULL,
 CONSTRAINT [PK_WebTextResources] PRIMARY KEY CLUSTERED 
(
	[TextResourceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTextResources_DirectoryId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTextResource] ADD  CONSTRAINT [DF_WebTextResources_DirectoryId]  DEFAULT ((-1)) FOR [DirectoryId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTextResources_Rank]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTextResource] ADD  CONSTRAINT [DF_WebTextResources_Rank]  DEFAULT ((0)) FOR [Rank]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTextResource_DateModified]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTextResource] ADD  CONSTRAINT [DF_WebTextResource_DateModified]  DEFAULT (getdate()) FOR [DateModified]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTextResource_OwnerObjectId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTextResource] ADD  CONSTRAINT [DF_WebTextResource_OwnerObjectId]  DEFAULT ((-1)) FOR [OwnerObjectId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTextResource_OwnerRecordId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTextResource] ADD  CONSTRAINT [DF_WebTextResource_OwnerRecordId]  DEFAULT ((-1)) FOR [OwnerRecordId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTextResource_DatePersisted]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTextResource] ADD  CONSTRAINT [DF_WebTextResource_DatePersisted]  DEFAULT (getdate()) FOR [DatePersisted]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebTextResource_PhysicalPath]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebTextResource] ADD  CONSTRAINT [DF_WebTextResource_PhysicalPath]  DEFAULT ('') FOR [PhysicalPath]
END

GO
