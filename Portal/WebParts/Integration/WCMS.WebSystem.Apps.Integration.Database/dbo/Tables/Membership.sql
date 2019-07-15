CREATE TABLE [dbo].[Membership] (
    [MembershipID]   BIGINT        IDENTITY (1, 1) NOT NULL,
    [MemberID]       BIGINT        NULL,
    [MemberTypeID]   SMALLINT      NULL,
    [MembershipStatusID] SMALLINT      NULL,
    [LocaleStatusID] SMALLINT      NULL,
    [LocaleID]       INT           NULL,
    [GroupID]     SMALLINT      NULL,
    [CommitteeID]    SMALLINT      NULL,
    [MembershipDate]    SMALLDATETIME NULL,
    [MembershipPlace]   VARCHAR (200) NULL,
    [OrientedBy]  VARCHAR (100) NULL,
    [OnboardedBy]     VARCHAR (100) NULL,
    [PreviousOrganization]    VARCHAR (200) NULL,
    CONSTRAINT [PK_Membership] PRIMARY KEY CLUSTERED ([MembershipID] ASC)
);

