SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebMessageQueue]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebMessageQueue](
	[Id] [int] NOT NULL,
	[FromObjectId] [int] NOT NULL,
	[FromRecordId] [int] NOT NULL,
	[EmailSubject] [nvarchar](4000) COLLATE Latin1_General_CI_AI NOT NULL,
	[EmailMessage] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SmsMessage] [nvarchar](4000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[To] [nvarchar](4000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ToFailed] [nvarchar](4000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ToExcluded] [nvarchar](4000) COLLATE Latin1_General_CI_AI NOT NULL,
	[ToOrBcc] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateSent] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[SendVia] [int] NOT NULL,
	[EnableMonitor] [int] NOT NULL,
 CONSTRAINT [PK_WebMessageQueue] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebMessageQueue_FromObjectId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebMessageQueue] ADD  CONSTRAINT [DF_WebMessageQueue_FromObjectId]  DEFAULT ((-1)) FOR [FromObjectId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebMessageQueue_FromRecordId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebMessageQueue] ADD  CONSTRAINT [DF_WebMessageQueue_FromRecordId]  DEFAULT ((-1)) FOR [FromRecordId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebMessageQueue_ToOrBcc]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebMessageQueue] ADD  CONSTRAINT [DF_WebMessageQueue_ToOrBcc]  DEFAULT ((0)) FOR [ToOrBcc]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebMessageQueue_DateCreated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebMessageQueue] ADD  CONSTRAINT [DF_WebMessageQueue_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebMessageQueue_DateSent]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebMessageQueue] ADD  CONSTRAINT [DF_WebMessageQueue_DateSent]  DEFAULT (getdate()) FOR [DateSent]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebMessageQueue_Status]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebMessageQueue] ADD  CONSTRAINT [DF_WebMessageQueue_Status]  DEFAULT ((0)) FOR [Status]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebMessageQueue_EnableMonitor]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebMessageQueue] ADD  CONSTRAINT [DF_WebMessageQueue_EnableMonitor]  DEFAULT ((1)) FOR [EnableMonitor]
END

GO
