SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebConstant]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebConstant](
	[ConstantId] [int] NOT NULL,
	[Value] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Rank] [int] NOT NULL,
	[Category] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Text] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ObjectId] [int] NOT NULL,
 CONSTRAINT [PK_WebConstants] PRIMARY KEY CLUSTERED 
(
	[ConstantId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebConsta__Category]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebConstant] ADD  CONSTRAINT [DF__WebConsta__Category]  DEFAULT ('') FOR [Category]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebConsta__ObjectId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebConstant] ADD  CONSTRAINT [DF__WebConsta__ObjectId]  DEFAULT ((-1)) FOR [ObjectId]
END

GO
