CREATE TABLE [dbo].[WebJob] (
    [Id]                 INT             CONSTRAINT [DF_WebJob_Id] DEFAULT ((-1)) NOT NULL,
    [Name]               NVARCHAR (250)  CONSTRAINT [DF_WebJob_Name] DEFAULT ('') NOT NULL,
    [RecurrenceId]       INT             CONSTRAINT [DF_WebJob_RecurrenceId] DEFAULT ((-1)) NOT NULL,
    [Weekdays]           INT             CONSTRAINT [DF_WebJob_Weekdays] DEFAULT ((0)) NOT NULL,
    [OccursEvery]        INT             CONSTRAINT [DF_WebJob_OccursEvery] DEFAULT ((1)) NOT NULL,
    [ExecutionStartDate] DATETIME        CONSTRAINT [DF_WebJob_ExecutionStartDate] DEFAULT (getdate()) NOT NULL,
    [ExecutionEndDate]   DATETIME        CONSTRAINT [DF_WebJob_ExecutionEndDate] DEFAULT (getdate()) NOT NULL,
    [ExecutionStatus]    INT             CONSTRAINT [DF_WebJob_ExecutionStatus] DEFAULT ((0)) NOT NULL,
    [ExecutionMessage]   NTEXT           CONSTRAINT [DF_WebJob_ExecutionMessage] DEFAULT ('') NOT NULL,
    [Enabled]            INT             CONSTRAINT [DF_WebJob_Enabled] DEFAULT ((1)) NOT NULL,
    [TypeName]           NVARCHAR (250)  CONSTRAINT [DF_WebJob_TypeName] DEFAULT ('') NOT NULL,
    [StartDate]          DATETIME        CONSTRAINT [DF_WebJob_StartDate] DEFAULT (getdate()) NOT NULL,
    [Description]        NVARCHAR (4000) COLLATE Latin1_General_CI_AI CONSTRAINT [DF_WebJob_Description] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_WebJob] PRIMARY KEY CLUSTERED ([Id] ASC)
);

