CREATE TABLE [dbo].[IncidentCategory] (
    [Id]          INT             NOT NULL,
    [Name]        NVARCHAR (500)  COLLATE Latin1_General_CI_AI NOT NULL,
    [GroupId]     INT             NOT NULL,
    [Description] NVARCHAR (4000) COLLATE Latin1_General_CI_AI NOT NULL,
    [Rank]        INT             CONSTRAINT [DF_IncidentCategory_Rank] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_IncidentCategory] PRIMARY KEY CLUSTERED ([Id] ASC)
);

