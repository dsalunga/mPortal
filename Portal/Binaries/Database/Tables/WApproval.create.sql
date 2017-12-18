SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WApproval]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WApproval](
	[Id] [int] NOT NULL,
	[ObjectId] [int] NOT NULL,
	[RecordId] [int] NOT NULL,
	[ApproverUserId] [int] NOT NULL,
	[ApprovalDate] [datetime] NOT NULL,
	[Comments] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WGroupApproval_DateApproved]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WApproval] ADD  CONSTRAINT [DF_WGroupApproval_DateApproved]  DEFAULT (getdate()) FOR [ApprovalDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WGroupApproval_Comments]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WApproval] ADD  CONSTRAINT [DF_WGroupApproval_Comments]  DEFAULT ('') FOR [Comments]
END

GO
