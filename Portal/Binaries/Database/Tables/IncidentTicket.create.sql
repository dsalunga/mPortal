SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IncidentTicket]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[IncidentTicket](
	[Id] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Description] [ntext] COLLATE Latin1_General_CI_AI NOT NULL,
	[Urgency] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[AssignedGroupId] [int] NOT NULL,
	[AssignedUserId] [int] NOT NULL,
	[TicketGuid] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DateClosed] [datetime] NOT NULL,
	[SubmitterId] [int] NOT NULL,
	[ETA] [datetime] NOT NULL,
	[TypeId] [int] NOT NULL,
	[NotifyAlso] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[InstanceId] [int] NOT NULL,
 CONSTRAINT [PK_IncidentTicket] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_IncidentTicket_GroupId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentTicket] ADD  CONSTRAINT [DF_IncidentTicket_GroupId]  DEFAULT ((-1)) FOR [AssignedGroupId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_IncidentTicket_AssignedUserId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentTicket] ADD  CONSTRAINT [DF_IncidentTicket_AssignedUserId]  DEFAULT ((-1)) FOR [AssignedUserId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_IncidentTicket_TicketGuid]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentTicket] ADD  CONSTRAINT [DF_IncidentTicket_TicketGuid]  DEFAULT ('') FOR [TicketGuid]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_IncidentTicket_SubmitterId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentTicket] ADD  CONSTRAINT [DF_IncidentTicket_SubmitterId]  DEFAULT ((-1)) FOR [SubmitterId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_IncidentTicket_ETA]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentTicket] ADD  CONSTRAINT [DF_IncidentTicket_ETA]  DEFAULT ('1800-01-01') FOR [ETA]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_IncidentTicket_TypeId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentTicket] ADD  CONSTRAINT [DF_IncidentTicket_TypeId]  DEFAULT ((-1)) FOR [TypeId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_IncidentTicket_NotifyAlso]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentTicket] ADD  CONSTRAINT [DF_IncidentTicket_NotifyAlso]  DEFAULT ('') FOR [NotifyAlso]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__IncidentT__Insta__6395DBC1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentTicket] ADD  DEFAULT ((-1)) FOR [InstanceId]
END

GO
