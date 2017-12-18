SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebJob]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebJob](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RecurrenceId] [int] NOT NULL,
	[Weekdays] [int] NOT NULL,
	[OccursEvery] [int] NOT NULL,
	[ExecutionStartDate] [datetime] NOT NULL,
	[ExecutionEndDate] [datetime] NOT NULL,
	[ExecutionStatus] [int] NOT NULL,
	[ExecutionMessage] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Enabled] [int] NOT NULL,
	[TypeName] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[Description] [nvarchar](4000) COLLATE Latin1_General_CI_AI NOT NULL,
 CONSTRAINT [PK_WebJob] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebJob_Id]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebJob] ADD  CONSTRAINT [DF_WebJob_Id]  DEFAULT ((-1)) FOR [Id]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebJob_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebJob] ADD  CONSTRAINT [DF_WebJob_Name]  DEFAULT ('') FOR [Name]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebJob_RecurrenceId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebJob] ADD  CONSTRAINT [DF_WebJob_RecurrenceId]  DEFAULT ((-1)) FOR [RecurrenceId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebJob_Weekdays]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebJob] ADD  CONSTRAINT [DF_WebJob_Weekdays]  DEFAULT ((0)) FOR [Weekdays]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebJob_OccursEvery]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebJob] ADD  CONSTRAINT [DF_WebJob_OccursEvery]  DEFAULT ((1)) FOR [OccursEvery]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebJob_ExecutionStartDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebJob] ADD  CONSTRAINT [DF_WebJob_ExecutionStartDate]  DEFAULT (getdate()) FOR [ExecutionStartDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebJob_ExecutionEndDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebJob] ADD  CONSTRAINT [DF_WebJob_ExecutionEndDate]  DEFAULT (getdate()) FOR [ExecutionEndDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebJob_ExecutionStatus]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebJob] ADD  CONSTRAINT [DF_WebJob_ExecutionStatus]  DEFAULT ((0)) FOR [ExecutionStatus]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebJob_ExecutionMessage]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebJob] ADD  CONSTRAINT [DF_WebJob_ExecutionMessage]  DEFAULT ('') FOR [ExecutionMessage]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebJob_Enabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebJob] ADD  CONSTRAINT [DF_WebJob_Enabled]  DEFAULT ((1)) FOR [Enabled]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebJob_TypeName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebJob] ADD  CONSTRAINT [DF_WebJob_TypeName]  DEFAULT ('') FOR [TypeName]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebJob_StartDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebJob] ADD  CONSTRAINT [DF_WebJob_StartDate]  DEFAULT (getdate()) FOR [StartDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebJob_Description]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebJob] ADD  CONSTRAINT [DF_WebJob_Description]  DEFAULT ('') FOR [Description]
END

GO
