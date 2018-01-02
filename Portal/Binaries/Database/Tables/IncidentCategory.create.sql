SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IncidentCategory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[IncidentCategory](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](500) COLLATE Latin1_General_CI_AI NOT NULL,
	[GroupId] [int] NOT NULL,
	[Description] [nvarchar](4000) COLLATE Latin1_General_CI_AI NOT NULL,
	[Rank] [int] NOT NULL,
	[InstanceId] [int] NOT NULL,
 CONSTRAINT [PK_IncidentCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_IncidentCategory_Rank]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentCategory] ADD  CONSTRAINT [DF_IncidentCategory_Rank]  DEFAULT ((0)) FOR [Rank]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__IncidentC__Insta__187221A6]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentCategory] ADD  DEFAULT ((-1)) FOR [InstanceId]
END

GO
