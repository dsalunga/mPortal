SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FileVersion]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[FileVersion](
	[Id] [int] NOT NULL,
	[FileId] [int] NOT NULL,
	[VersionDate] [datetime] NOT NULL,
	[Activity] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_FileVersion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_FileVersion_VersionDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[FileVersion] ADD  CONSTRAINT [DF_FileVersion_VersionDate]  DEFAULT (getdate()) FOR [VersionDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_FileVersion_Activity]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[FileVersion] ADD  CONSTRAINT [DF_FileVersion_Activity]  DEFAULT ((0)) FOR [Activity]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_FileVersion_UserId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[FileVersion] ADD  CONSTRAINT [DF_FileVersion_UserId]  DEFAULT ((-1)) FOR [UserId]
END

GO
