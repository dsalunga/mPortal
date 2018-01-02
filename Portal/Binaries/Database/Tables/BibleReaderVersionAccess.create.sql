SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BibleReaderVersionAccess]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BibleReaderVersionAccess](
	[Id] [int] NOT NULL,
	[BibleAccessId] [int] NOT NULL,
	[BibleVersionId] [int] NOT NULL,
	[BibleVersionName] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[LastAccessed] [datetime] NOT NULL,
	[VersionAccessCount] [int] NOT NULL,
 CONSTRAINT [PK_BibleReaderVersionAccess] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_BibleReaderVersionAccess_BibleVersionId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BibleReaderVersionAccess] ADD  CONSTRAINT [DF_BibleReaderVersionAccess_BibleVersionId]  DEFAULT ((-1)) FOR [BibleVersionId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_BibleReaderVersionAccess_BibleVersionName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BibleReaderVersionAccess] ADD  CONSTRAINT [DF_BibleReaderVersionAccess_BibleVersionName]  DEFAULT ('') FOR [BibleVersionName]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_BibleReaderVersionAccess_LastAccess]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BibleReaderVersionAccess] ADD  CONSTRAINT [DF_BibleReaderVersionAccess_LastAccess]  DEFAULT (getdate()) FOR [LastAccessed]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_BibleReaderVersionAccess_VersionAccessCount]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BibleReaderVersionAccess] ADD  CONSTRAINT [DF_BibleReaderVersionAccess_VersionAccessCount]  DEFAULT ((-1)) FOR [VersionAccessCount]
END

GO
