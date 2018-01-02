SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Member]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Member](
	[MemberID] [bigint] NOT NULL,
	[ExternalIDNo] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TemporaryIDNo] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FirstName] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[MiddleName] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[LastName] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[BirthDate] [datetime] NULL,
	[BirthPlace] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Gender] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BloodType] [varchar](3) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CivilStatusID] [int] NULL,
	[CitizenshipID] [int] NULL,
	[RaceID] [int] NULL,
	[Phone] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Mobile] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Email] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsActive] [int] NULL,
	[Flag] [char](1) COLLATE Latin1_General_CI_AI NOT NULL,
	[NickName] [nvarchar](250) COLLATE Latin1_General_CI_AI NULL,
	[DateCreated] [datetime] NULL,
	[DateUpdated] [datetime] NULL,
	[MembershipDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Member] PRIMARY KEY CLUSTERED 
(
	[MemberID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Member_Flag]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Member] ADD  CONSTRAINT [DF_Member_Flag]  DEFAULT ('M') FOR [Flag]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Member_MembershipDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Member] ADD  CONSTRAINT [DF_Member_MembershipDate]  DEFAULT (getdate()) FOR [MembershipDate]
END

GO
