SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IncidentInstance]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[IncidentInstance](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](500) COLLATE Latin1_General_CI_AI NOT NULL,
	[IncidentPrefix] [nvarchar](500) COLLATE Latin1_General_CI_AI NOT NULL,
	[BaseGroup] [nvarchar](500) COLLATE Latin1_General_CI_AI NOT NULL,
	[SupportGroupPath] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SLAHighDuration] [int] NOT NULL,
	[SLALowDuration] [int] NOT NULL,
	[SLANormalDuration] [int] NOT NULL,
	[SLAWarningPercentage] [decimal](5, 4) NOT NULL,
 CONSTRAINT [PK_IncidentInstance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__IncidentI__Incid__2A5D5E65]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentInstance] ADD  DEFAULT ('INC') FOR [IncidentPrefix]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__IncidentI__SLAHi__2B51829E]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentInstance] ADD  DEFAULT ((24)) FOR [SLAHighDuration]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__IncidentI__SLALo__2C45A6D7]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentInstance] ADD  DEFAULT ((168)) FOR [SLALowDuration]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__IncidentI__SLANo__2D39CB10]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentInstance] ADD  DEFAULT ((72)) FOR [SLANormalDuration]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__IncidentI__SLAWa__2E2DEF49]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentInstance] ADD  DEFAULT ((0.8)) FOR [SLAWarningPercentage]
END

GO
