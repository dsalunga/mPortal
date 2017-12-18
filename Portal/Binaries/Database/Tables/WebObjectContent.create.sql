SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebObjectContent]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebObjectContent](
	[ObjectContentId] [int] NOT NULL,
	[ObjectId] [int] NOT NULL,
	[ContentId] [int] NOT NULL,
	[RecordId] [int] NOT NULL,
 CONSTRAINT [PK_WebObjectContents] PRIMARY KEY CLUSTERED 
(
	[ObjectContentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
