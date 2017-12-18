SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventLog]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EventLog](
	[Id] [int] NOT NULL,
	[EventDate] [datetime] NOT NULL,
	[Content] [ntext] COLLATE Latin1_General_CI_AS NOT NULL,
	[UserId] [int] NOT NULL,
	[EventName] [nvarchar](250) COLLATE Latin1_General_CI_AS NOT NULL,
	[IPAddress] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_EventLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_AuditLog_LogDateTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventLog] ADD  CONSTRAINT [DF_AuditLog_LogDateTime]  DEFAULT (getdate()) FOR [EventDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_AuditLog_Content]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventLog] ADD  CONSTRAINT [DF_AuditLog_Content]  DEFAULT ('') FOR [Content]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_EventLog_UserId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventLog] ADD  CONSTRAINT [DF_EventLog_UserId]  DEFAULT ((-1)) FOR [UserId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_EventLog_Action]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventLog] ADD  CONSTRAINT [DF_EventLog_Action]  DEFAULT ('') FOR [EventName]
END

GO
