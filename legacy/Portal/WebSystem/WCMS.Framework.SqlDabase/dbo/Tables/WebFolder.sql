CREATE TABLE [dbo].[WebFolder] (
    [Id]        INT            NOT NULL,
    [Name]      NVARCHAR (250) NOT NULL,
    [ParentId]  INT            NOT NULL,
    [ShareName] NVARCHAR (250) COLLATE Latin1_General_CI_AI CONSTRAINT [DF_WebFolder_ShareName] DEFAULT ('') NOT NULL,
    [ObjectId]  INT            CONSTRAINT [DF_WebFolder_ObjectId] DEFAULT ((-1)) NOT NULL,
    [SiteId]    INT            CONSTRAINT [DF_WebFolder_SiteId] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_WebFolder] PRIMARY KEY CLUSTERED ([Id] ASC)
);

