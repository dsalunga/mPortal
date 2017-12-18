CREATE TABLE [dbo].[IncidentType] (
    [Id]           INT            NOT NULL,
    [Name]         NVARCHAR (500) NOT NULL,
    [FollowStdSLA] INT            CONSTRAINT [DF_IncidentType_FollowStdSla] DEFAULT ((1)) NOT NULL,
    [Rank]         INT            CONSTRAINT [DF_IncidentType_Rank] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_IncidentType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

