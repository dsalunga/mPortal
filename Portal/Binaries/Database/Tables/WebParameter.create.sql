SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebParameter]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebParameter](
	[Id] [int] NOT NULL,
	[ObjectId] [int] NOT NULL,
	[RecordId] [int] NOT NULL,
	[Name] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Value] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[IsRequired] [int] NOT NULL,
 CONSTRAINT [PK_WebParameter] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebParameter_IsRequired]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebParameter] ADD  CONSTRAINT [DF_WebParameter_IsRequired]  DEFAULT ((0)) FOR [IsRequired]
END

GO
