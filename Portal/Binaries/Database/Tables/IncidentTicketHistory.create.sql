SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IncidentTicketHistory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[IncidentTicketHistory](
	[Id] [int] NOT NULL,
	[TicketId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Content] [nvarchar](4000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[Type] [int] NOT NULL,
 CONSTRAINT [PK_IncidentTicketHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_IncidentTicketHistory_IsNote]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentTicketHistory] ADD  CONSTRAINT [DF_IncidentTicketHistory_IsNote]  DEFAULT ((0)) FOR [Type]
END

GO
