SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FileIdentity]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[FileIdentity](
	[Id] [int] NOT NULL,
	[ObjectId] [int] NOT NULL,
	[RecordId] [int] NOT NULL,
	[LibraryId] [int] NOT NULL,
	[FilePath] [nvarchar](4000) COLLATE Latin1_General_CI_AI NOT NULL,
	[Name] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_FileIdentity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_FileIdentity_ObjectId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[FileIdentity] ADD  CONSTRAINT [DF_FileIdentity_ObjectId]  DEFAULT ((-1)) FOR [ObjectId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_FileIdentity_RecordId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[FileIdentity] ADD  CONSTRAINT [DF_FileIdentity_RecordId]  DEFAULT ((-1)) FOR [RecordId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_FileIdentity_LibraryId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[FileIdentity] ADD  CONSTRAINT [DF_FileIdentity_LibraryId]  DEFAULT ((-1)) FOR [LibraryId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_FileIdentity_FilePath]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[FileIdentity] ADD  CONSTRAINT [DF_FileIdentity_FilePath]  DEFAULT ('') FOR [FilePath]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_FileIdentity_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[FileIdentity] ADD  CONSTRAINT [DF_FileIdentity_Name]  DEFAULT ('') FOR [Name]
END

GO
