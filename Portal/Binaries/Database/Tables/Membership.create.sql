SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Membership]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Membership](
	[MembershipID] [bigint] IDENTITY(1,1) NOT NULL,
	[MemberID] [bigint] NULL,
	[MemberTypeID] [smallint] NULL,
	[MembershipStatusID] [smallint] NULL,
	[LocaleStatusID] [smallint] NULL,
	[LocaleID] [int] NULL,
	[GroupID] [smallint] NULL,
	[CommitteeID] [smallint] NULL,
	[MembershipDate] [smalldatetime] NULL,
	[MembershipPlace] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OrientedBy] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OnboardedBy] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PreviousOrganization] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Membership] PRIMARY KEY CLUSTERED 
(
	[MembershipID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
