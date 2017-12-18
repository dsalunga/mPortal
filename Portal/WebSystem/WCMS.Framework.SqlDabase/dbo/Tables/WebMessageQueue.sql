CREATE TABLE [dbo].[WebMessageQueue] (
    [Id]           INT             NOT NULL,
    [FromObjectId] INT             CONSTRAINT [DF_WebMessageQueue_FromObjectId] DEFAULT ((-1)) NOT NULL,
    [FromRecordId] INT             CONSTRAINT [DF_WebMessageQueue_FromRecordId] DEFAULT ((-1)) NOT NULL,
    [EmailSubject] NVARCHAR (4000) COLLATE Latin1_General_CI_AI NOT NULL,
    [EmailMessage] NTEXT           NOT NULL,
    [SmsMessage]   NVARCHAR (4000) NOT NULL,
    [To]           NVARCHAR (4000) NOT NULL,
    [ToFailed]     NVARCHAR (4000) NOT NULL,
    [ToExcluded]   NVARCHAR (4000) COLLATE Latin1_General_CI_AI NOT NULL,
    [ToOrBcc]      INT             CONSTRAINT [DF_WebMessageQueue_ToOrBcc] DEFAULT ((0)) NOT NULL,
    [DateCreated]  DATETIME        CONSTRAINT [DF_WebMessageQueue_DateCreated] DEFAULT (getdate()) NOT NULL,
    [DateSent]     DATETIME        CONSTRAINT [DF_WebMessageQueue_DateSent] DEFAULT (getdate()) NOT NULL,
    [Status]       INT             CONSTRAINT [DF_WebMessageQueue_Status] DEFAULT ((0)) NOT NULL,
    [SendVia]      INT             NOT NULL,
    CONSTRAINT [PK_WebMessageQueue] PRIMARY KEY CLUSTERED ([Id] ASC)
);

