SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GenericListColumnOption]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GenericListColumnOption](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ColumnId] [int] NOT NULL,
	[OptionTypeId] [int] NOT NULL,
	[Rank] [int] NOT NULL,
	[Caption] [nvarchar](2000) COLLATE Latin1_General_CI_AI NULL,
	[DefaultValue] [int] NULL,
 CONSTRAINT [PK_GenericListColumnOption] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
