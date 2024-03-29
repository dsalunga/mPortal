SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GenericListField]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GenericListField](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RowId] [int] NOT NULL,
	[ColumnId] [int] NOT NULL,
	[Answer] [nvarchar](max) COLLATE Latin1_General_CI_AI NULL,
 CONSTRAINT [PK_GenericListField] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
