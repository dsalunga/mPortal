SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebAttachment]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebAttachment](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](500) COLLATE Latin1_General_CI_AI NOT NULL,
	[FilePath] [nvarchar](500) COLLATE Latin1_General_CI_AI NOT NULL,
	[Size] [bigint] NOT NULL,
	[DateUploaded] [datetime] NOT NULL,
	[UserId] [int] NOT NULL,
	[ObjectId] [int] NOT NULL,
	[RecordId] [int] NOT NULL,
	[BatchGuid] [nvarchar](50) COLLATE Latin1_General_CI_AI NOT NULL,
 CONSTRAINT [PK_WebAttachment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
