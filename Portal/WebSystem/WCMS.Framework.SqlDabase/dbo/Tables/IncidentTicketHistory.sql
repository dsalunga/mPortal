CREATE TABLE [dbo].[IncidentTicketHistory] (
    [Id]          INT             NOT NULL,
    [TicketId]    INT             NOT NULL,
    [UserId]      INT             NOT NULL,
    [Content]     NVARCHAR (4000) NOT NULL,
    [DateCreated] DATETIME        NOT NULL,
    [Type]        INT             CONSTRAINT [DF_IncidentTicketHistory_IsNote] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_IncidentTicketHistory] PRIMARY KEY CLUSTERED ([Id] ASC)
);

