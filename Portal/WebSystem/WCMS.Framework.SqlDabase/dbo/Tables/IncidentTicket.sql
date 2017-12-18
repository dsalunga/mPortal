CREATE TABLE [dbo].[IncidentTicket] (
    [Id]              INT           NOT NULL,
    [UserId]          INT           NOT NULL,
    [DateCreated]     DATETIME      NOT NULL,
    [CategoryId]      INT           NOT NULL,
    [Description]     NTEXT         COLLATE Latin1_General_CI_AI NOT NULL,
    [Urgency]         INT           NOT NULL,
    [Status]          INT           NOT NULL,
    [AssignedGroupId] INT           CONSTRAINT [DF_IncidentTicket_GroupId] DEFAULT ((-1)) NOT NULL,
    [AssignedUserId]  INT           CONSTRAINT [DF_IncidentTicket_AssignedUserId] DEFAULT ((-1)) NOT NULL,
    [TicketGuid]      NVARCHAR (50) CONSTRAINT [DF_IncidentTicket_TicketGuid] DEFAULT ('') NOT NULL,
    [DateClosed]      DATETIME      NOT NULL,
    [SubmitterId]     INT           CONSTRAINT [DF_IncidentTicket_SubmitterId] DEFAULT ((-1)) NOT NULL,
    [ETA]             DATETIME      CONSTRAINT [DF_IncidentTicket_ETA] DEFAULT ('1800-01-01') NOT NULL,
    [TypeId]          INT           CONSTRAINT [DF_IncidentTicket_TypeId] DEFAULT ((-1)) NOT NULL,
    [NotifyAlso]      NTEXT         CONSTRAINT [DF_IncidentTicket_NotifyAlso] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_IncidentTicket] PRIMARY KEY CLUSTERED ([Id] ASC)
);

