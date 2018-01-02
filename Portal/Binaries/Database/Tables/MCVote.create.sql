SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MCVote]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MCVote](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FirstName] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[LastName] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[MobileNumber] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Email] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CandidateId] [int] NOT NULL,
	[DateVoted] [datetime] NOT NULL,
	[UserName] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Status] [int] NOT NULL,
	[CompetitionId] [int] NOT NULL,
	[IPAddress] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Spam] [int] NOT NULL,
 CONSTRAINT [PK_MCVote] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCVote_Code]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCVote] ADD  CONSTRAINT [DF_MCVote_Code]  DEFAULT ('') FOR [Code]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCVote_FirstName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCVote] ADD  CONSTRAINT [DF_MCVote_FirstName]  DEFAULT ('') FOR [FirstName]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCVote_LastName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCVote] ADD  CONSTRAINT [DF_MCVote_LastName]  DEFAULT ('') FOR [LastName]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCVote_MobileNumber]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCVote] ADD  CONSTRAINT [DF_MCVote_MobileNumber]  DEFAULT ('') FOR [MobileNumber]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCVote_Email]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCVote] ADD  CONSTRAINT [DF_MCVote_Email]  DEFAULT ('') FOR [Email]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCVote_CandidateId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCVote] ADD  CONSTRAINT [DF_MCVote_CandidateId]  DEFAULT ((-1)) FOR [CandidateId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MCVote_UserName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCVote] ADD  CONSTRAINT [DF_MCVote_UserName]  DEFAULT ('') FOR [UserName]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCVote__Status]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCVote] ADD  CONSTRAINT [DF__MCVote__Status]  DEFAULT ((0)) FOR [Status]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCVote__CompetitionId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCVote] ADD  CONSTRAINT [DF__MCVote__CompetitionId]  DEFAULT ((-1)) FOR [CompetitionId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCVote__IPAddress]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCVote] ADD  CONSTRAINT [DF__MCVote__IPAddress]  DEFAULT ('') FOR [IPAddress]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MCVote__Spam]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MCVote] ADD  CONSTRAINT [DF__MCVote__Spam]  DEFAULT ((0)) FOR [Spam]
END

GO
