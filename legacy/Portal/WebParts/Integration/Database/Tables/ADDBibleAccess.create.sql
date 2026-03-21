SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BibleReaderAccess]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BibleReaderAccess](
	[Id] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[AppAccessCount] [int] NOT NULL,
	[LastAccessed] [datetime] NOT NULL,
 CONSTRAINT [PK_BibleReaderAccess] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_BibleReaderAccess_AppAccessCount]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BibleReaderAccess] ADD  CONSTRAINT [DF_BibleReaderAccess_AppAccessCount]  DEFAULT ((-1)) FOR [AppAccessCount]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_BibleReaderAccess_LastAccess]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BibleReaderAccess] ADD  CONSTRAINT [DF_BibleReaderAccess_LastAccess]  DEFAULT (getdate()) FOR [LastAccessed]
END

GO
