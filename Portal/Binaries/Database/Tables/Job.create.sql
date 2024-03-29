SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Job]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Job](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](2000) COLLATE Latin1_General_CI_AI NOT NULL,
	[Description] [nvarchar](max) COLLATE Latin1_General_CI_AI NOT NULL,
 CONSTRAINT [PK_Job] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
