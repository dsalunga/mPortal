CREATE TABLE [dbo].[WebTab] (
    [Id]       INT            NOT NULL,
    [Text]     NVARCHAR (250) COLLATE Latin1_General_CI_AI CONSTRAINT [DF_WebTab_Text] DEFAULT ('') NOT NULL,
    [Target]   NVARCHAR (250) COLLATE Latin1_General_CI_AI CONSTRAINT [DF_WebTab_Target] DEFAULT ('') NOT NULL,
    [Rank]     INT            CONSTRAINT [DF_WebTab_Rank] DEFAULT ((0)) NOT NULL,
    [TabSetId] INT            CONSTRAINT [DF_WebTab_TabSetId] DEFAULT ((-1)) NOT NULL,
    [Name]     NVARCHAR (250) COLLATE Latin1_General_CI_AI CONSTRAINT [DF_WebTab_Name] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_WebTab] PRIMARY KEY CLUSTERED ([Id] ASC)
);

