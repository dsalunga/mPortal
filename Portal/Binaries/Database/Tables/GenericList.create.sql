SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GenericList]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GenericList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) COLLATE Latin1_General_CI_AI NOT NULL,
	[Description] [ntext] COLLATE Latin1_General_CI_AI NULL,
	[IsActive] [int] NOT NULL,
	[EndingText] [nvarchar](2000) COLLATE Latin1_General_CI_AI NULL,
	[ShowPageCaption] [int] NOT NULL,
 CONSTRAINT [PK_GenericList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
