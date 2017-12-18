SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IncidentType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[IncidentType](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FollowStdSLA] [int] NOT NULL,
	[Rank] [int] NOT NULL,
	[InstanceId] [int] NOT NULL,
 CONSTRAINT [PK_IncidentType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_IncidentType_FollowStdSla]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentType] ADD  CONSTRAINT [DF_IncidentType_FollowStdSla]  DEFAULT ((1)) FOR [FollowStdSLA]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_IncidentType_Rank]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentType] ADD  CONSTRAINT [DF_IncidentType_Rank]  DEFAULT ((1)) FOR [Rank]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__IncidentT__Insta__5083074D]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentType] ADD  DEFAULT ((-1)) FOR [InstanceId]
END

GO
