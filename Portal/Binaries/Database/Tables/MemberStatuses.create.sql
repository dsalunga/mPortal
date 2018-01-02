SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MemberStatuses]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MemberStatuses](
	[MemberStatusID] [bigint] IDENTITY(1,1) NOT NULL,
	[MemberID] [bigint] NULL,
	[MemberTypeID] [smallint] NULL,
	[MembershipStatusID] [smallint] NULL,
	[LocaleStatusID] [smallint] NULL,
	[LocaleID] [int] NULL,
	[GroupID] [smallint] NULL,
	[CommitteeID] [smallint] NULL,
	[MembershipDate] [smalldatetime] NULL,
	[MembershipPlace] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OrientedByID] [bigint] NULL,
	[OnboardedByID] [bigint] NULL,
	[PreviousOrganizationID] [smallint] NULL,
	[WithID] [bit] NULL,
 CONSTRAINT [PK_MemberStatuses] PRIMARY KEY CLUSTERED 
(
	[MemberStatusID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
