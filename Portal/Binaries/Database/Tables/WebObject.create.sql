SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebObject]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebObject](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[IdentityColumn] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ObjectType] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Owner] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Prefix] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[LastRecordId] [int] NOT NULL,
	[MaxCacheCount] [int] NOT NULL,
	[AccessTypeId] [int] NOT NULL,
	[CacheTypeId] [int] NOT NULL,
	[MaxHistoryCount] [int] NOT NULL,
	[DataProviderName] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TypeName] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CacheInterval] [int] NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[ManagerName] [nvarchar](250) COLLATE Latin1_General_CI_AI NOT NULL,
	[NameColumn] [nvarchar](250) COLLATE Latin1_General_CI_AI NOT NULL,
	[FriendlyName] [nvarchar](250) COLLATE Latin1_General_CI_AI NOT NULL,
 CONSTRAINT [PK_WebObjects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObjects_ObjectType]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObject] ADD  CONSTRAINT [DF_WebObjects_ObjectType]  DEFAULT ('T') FOR [ObjectType]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObject_Prefix]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObject] ADD  CONSTRAINT [DF_WebObject_Prefix]  DEFAULT ('') FOR [Prefix]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObjects_LastRecordId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObject] ADD  CONSTRAINT [DF_WebObjects_LastRecordId]  DEFAULT ((0)) FOR [LastRecordId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObjects_MaxCacheSize]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObject] ADD  CONSTRAINT [DF_WebObjects_MaxCacheSize]  DEFAULT ((-1)) FOR [MaxCacheCount]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObjects_AccessTypeId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObject] ADD  CONSTRAINT [DF_WebObjects_AccessTypeId]  DEFAULT ((-1)) FOR [AccessTypeId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObjects_CacheTypeId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObject] ADD  CONSTRAINT [DF_WebObjects_CacheTypeId]  DEFAULT ((-1)) FOR [CacheTypeId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObjects_MaxHistorySize]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObject] ADD  CONSTRAINT [DF_WebObjects_MaxHistorySize]  DEFAULT ((-1)) FOR [MaxHistoryCount]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObject_DataProviderName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObject] ADD  CONSTRAINT [DF_WebObject_DataProviderName]  DEFAULT ('') FOR [DataProviderName]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObject_TypeName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObject] ADD  CONSTRAINT [DF_WebObject_TypeName]  DEFAULT ('') FOR [TypeName]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObject_CacheInterval]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObject] ADD  CONSTRAINT [DF_WebObject_CacheInterval]  DEFAULT ((-1)) FOR [CacheInterval]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObject_DateModified]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObject] ADD  CONSTRAINT [DF_WebObject_DateModified]  DEFAULT (getdate()) FOR [DateModified]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObject_ManagerName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObject] ADD  CONSTRAINT [DF_WebObject_ManagerName]  DEFAULT ('') FOR [ManagerName]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObject_NameColumn]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObject] ADD  CONSTRAINT [DF_WebObject_NameColumn]  DEFAULT ('') FOR [NameColumn]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObject_FriendlyName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObject] ADD  CONSTRAINT [DF_WebObject_FriendlyName]  DEFAULT ('') FOR [FriendlyName]
END

GO
