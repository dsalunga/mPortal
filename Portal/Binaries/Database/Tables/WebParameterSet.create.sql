SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebParameterSet]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebParameterSet](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](250) COLLATE Latin1_General_CI_AI NOT NULL,
	[SchemaParameterName] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_WebParameterSet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebParameterSet_Id]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebParameterSet] ADD  CONSTRAINT [DF_WebParameterSet_Id]  DEFAULT ((-1)) FOR [Id]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebParameterSet_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebParameterSet] ADD  CONSTRAINT [DF_WebParameterSet_Name]  DEFAULT ('') FOR [Name]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebParameterSet_SchemaParameterName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebParameterSet] ADD  CONSTRAINT [DF_WebParameterSet_SchemaParameterName]  DEFAULT ('') FOR [SchemaParameterName]
END

GO
