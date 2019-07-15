CREATE TABLE [dbo].[ODKVisit] (
    [Id]            INT             CONSTRAINT [DF_ODKVisit_Id] DEFAULT ((-1)) NOT NULL,
    [CreatedUserId] INT             CONSTRAINT [DF_Table1_UserId] DEFAULT ((-1)) NOT NULL,
    [DateCreated]   DATETIME        CONSTRAINT [DF_ODKVisit_DateCreated] DEFAULT (getdate()) NOT NULL,
    [ActualReport]  NTEXT           COLLATE Latin1_General_CI_AI CONSTRAINT [DF_Table1_VisitReport] DEFAULT ('') NOT NULL,
    [Status]        NTEXT           COLLATE Latin1_General_CI_AI CONSTRAINT [DF_Table1_Status] DEFAULT ('') NOT NULL,
    [GroupId]       INT             CONSTRAINT [DF_ODKVisit_GroupId] DEFAULT ((-1)) NOT NULL,
    [Name]          NVARCHAR (250)  COLLATE Latin1_General_CI_AI CONSTRAINT [DF_Table1_MemberName] DEFAULT ('') NOT NULL,
    [VisitedUserId] INT             CONSTRAINT [DF_Table1_MemberUserId] DEFAULT ((-1)) NOT NULL,
    [DateVisited]   DATETIME        CONSTRAINT [DF_ODKVisit_DateVisited] DEFAULT (getdate()) NOT NULL,
    [ActionTaken]   NTEXT           COLLATE Latin1_General_CI_AS CONSTRAINT [DF_ODKVisit_ActionTaken] DEFAULT ('') NOT NULL,
    [ContactNo]     NVARCHAR (50)   COLLATE Latin1_General_CI_AS CONSTRAINT [DF_ODKVisit_ContactNo] DEFAULT ('') NOT NULL,
    [TimesVisited]  INT             CONSTRAINT [DF_ODKVisit_TimesVisited] DEFAULT ((0)) NOT NULL,
    [Address]       NVARCHAR (250)  COLLATE Latin1_General_CI_AS CONSTRAINT [DF_ODKVisit_Address] DEFAULT ('') NOT NULL,
    [MembershipDate]   DATETIME        CONSTRAINT [DF_ODKVisit_MembershipDate] DEFAULT (((1900)-(1))-(1)) NOT NULL,
    [Tags]          NVARCHAR (1000) CONSTRAINT [DF_ODKVisit_Tags] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_ODKVisit] PRIMARY KEY CLUSTERED ([Id] ASC)
);

