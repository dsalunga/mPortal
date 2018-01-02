SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ODKVisit]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ODKVisit](
	[Id] [int] NOT NULL,
	[CreatedUserId] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[ActualReport] [ntext] COLLATE Latin1_General_CI_AI NOT NULL,
	[Status] [ntext] COLLATE Latin1_General_CI_AI NOT NULL,
	[GroupId] [int] NOT NULL,
	[Name] [nvarchar](250) COLLATE Latin1_General_CI_AI NOT NULL,
	[VisitedUserId] [int] NOT NULL,
	[DateVisited] [datetime] NOT NULL,
	[ActionTaken] [ntext] COLLATE Latin1_General_CI_AS NOT NULL,
	[ContactNo] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
	[TimesVisited] [int] NOT NULL,
	[Address] [nvarchar](250) COLLATE Latin1_General_CI_AS NOT NULL,
	[MembershipDate] [datetime] NOT NULL,
	[Tags] [nvarchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_ODKVisit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ODKVisit_Id]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ODKVisit] ADD  CONSTRAINT [DF_ODKVisit_Id]  DEFAULT ((-1)) FOR [Id]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table1_UserId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ODKVisit] ADD  CONSTRAINT [DF_Table1_UserId]  DEFAULT ((-1)) FOR [CreatedUserId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ODKVisit_DateCreated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ODKVisit] ADD  CONSTRAINT [DF_ODKVisit_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table1_VisitReport]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ODKVisit] ADD  CONSTRAINT [DF_Table1_VisitReport]  DEFAULT ('') FOR [ActualReport]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table1_Status]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ODKVisit] ADD  CONSTRAINT [DF_Table1_Status]  DEFAULT ('') FOR [Status]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ODKVisit_GroupId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ODKVisit] ADD  CONSTRAINT [DF_ODKVisit_GroupId]  DEFAULT ((-1)) FOR [GroupId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table1_MemberName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ODKVisit] ADD  CONSTRAINT [DF_Table1_MemberName]  DEFAULT ('') FOR [Name]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table1_MemberUserId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ODKVisit] ADD  CONSTRAINT [DF_Table1_MemberUserId]  DEFAULT ((-1)) FOR [VisitedUserId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ODKVisit_DateVisited]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ODKVisit] ADD  CONSTRAINT [DF_ODKVisit_DateVisited]  DEFAULT (getdate()) FOR [DateVisited]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ODKVisit_ActionTaken]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ODKVisit] ADD  CONSTRAINT [DF_ODKVisit_ActionTaken]  DEFAULT ('') FOR [ActionTaken]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ODKVisit_ContactNo]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ODKVisit] ADD  CONSTRAINT [DF_ODKVisit_ContactNo]  DEFAULT ('') FOR [ContactNo]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ODKVisit_TimesVisited]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ODKVisit] ADD  CONSTRAINT [DF_ODKVisit_TimesVisited]  DEFAULT ((0)) FOR [TimesVisited]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ODKVisit_Address]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ODKVisit] ADD  CONSTRAINT [DF_ODKVisit_Address]  DEFAULT ('') FOR [Address]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ODKVisit_MembershipDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ODKVisit] ADD  CONSTRAINT [DF_ODKVisit_MembershipDate]  DEFAULT (((1900)-(1))-(1)) FOR [MembershipDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ODKVisit_Tags]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ODKVisit] ADD  CONSTRAINT [DF_ODKVisit_Tags]  DEFAULT ('') FOR [Tags]
END

GO
