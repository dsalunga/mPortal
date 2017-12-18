CREATE TABLE [dbo].[EventLog] (
    [Id]        INT            NOT NULL,
    [EventDate] DATETIME       CONSTRAINT [DF_AuditLog_LogDateTime] DEFAULT (getdate()) NOT NULL,
    [Content]   NTEXT          COLLATE Latin1_General_CI_AS CONSTRAINT [DF_AuditLog_Content] DEFAULT ('') NOT NULL,
    [UserId]    INT            CONSTRAINT [DF_EventLog_UserId] DEFAULT ((-1)) NOT NULL,
    [EventName] NVARCHAR (250) COLLATE Latin1_General_CI_AS CONSTRAINT [DF_EventLog_Action] DEFAULT ('') NOT NULL,
    [IPAddress] NVARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_EventLog] PRIMARY KEY CLUSTERED ([Id] ASC)
);

