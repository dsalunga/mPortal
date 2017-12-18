CREATE TABLE [dbo].[WebTheme] (
    [Id]         INT            NOT NULL,
    [TemplateId] INT            CONSTRAINT [DF_WebTheme_TemplateId] DEFAULT ((-1)) NOT NULL,
    [Name]       NVARCHAR (500) CONSTRAINT [DF_WebTheme_Name] DEFAULT ('') NOT NULL,
    [ParentId]   INT            CONSTRAINT [DF__WebTheme__ParentId] DEFAULT ((-1)) NOT NULL,
    [Identity]   NVARCHAR (500) CONSTRAINT [DF__WebTheme__Identiy] DEFAULT ('') NOT NULL,
    [SkinId]     INT            CONSTRAINT [DF__WebTheme__SkinId] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_WebTheme] PRIMARY KEY CLUSTERED ([Id] ASC)
);



