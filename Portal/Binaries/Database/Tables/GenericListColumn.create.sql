SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GenericListColumn]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GenericListColumn](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ListId] [int] NOT NULL,
	[PartitionId] [int] NOT NULL,
	[Rank] [int] NOT NULL,
	[Label] [nvarchar](2500) COLLATE Latin1_General_CI_AI NOT NULL,
	[IsHorizontal] [int] NOT NULL,
	[IsRequired] [int] NOT NULL,
 CONSTRAINT [PK_GenericListColumn] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
