SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebObjectHeader]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebObjectHeader](
	[ObjectHeaderId] [int] NOT NULL,
	[ObjectId] [int] NOT NULL,
	[RecordId] [int] NOT NULL,
	[TextResourceId] [int] NOT NULL,
 CONSTRAINT [PK_WebObjectTextResources] PRIMARY KEY CLUSTERED 
(
	[ObjectHeaderId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
