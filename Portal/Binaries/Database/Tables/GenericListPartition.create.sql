SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GenericListPartition]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GenericListPartition](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ListId] [int] NOT NULL,
	[Rank] [int] NOT NULL,
	[Title] [nvarchar](256) COLLATE Latin1_General_CI_AI NULL,
	[Description] [nvarchar](2000) COLLATE Latin1_General_CI_AI NULL,
	[ActionOptionId] [int] NULL,
	[ActionPartitionId] [int] NULL,
	[ActionOptionValue] [nvarchar](50) COLLATE Latin1_General_CI_AI NULL,
 CONSTRAINT [PK_GenericListPartition] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
