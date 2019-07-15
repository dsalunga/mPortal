CREATE TABLE [dbo].[MemberStatuses] (
    [MemberStatusID]  BIGINT        IDENTITY (1, 1) NOT NULL,
    [MemberID]        BIGINT        NULL,
    [MemberTypeID]    SMALLINT      NULL,
    [MembershipStatusID]  SMALLINT      NULL,
    [LocaleStatusID]  SMALLINT      NULL,
    [LocaleID]        INT           NULL,
    [GroupID]      SMALLINT      NULL,
    [CommitteeID]     SMALLINT      NULL,
    [MembershipDate]     SMALLDATETIME NULL,
    [MembershipPlace]    VARCHAR (200) NULL,
    [OrientedByID] BIGINT        NULL,
    [OnboardedByID]    BIGINT        NULL,
    [PreviousOrganizationID]   SMALLINT      NULL,
    [WithID]          BIT           NULL,
    CONSTRAINT [PK_MemberStatuses] PRIMARY KEY CLUSTERED ([MemberStatusID] ASC)
);

