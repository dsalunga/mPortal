CREATE TABLE [dbo].[WebTemplate] (
    [Id]               INT            NOT NULL,
    [Name]             NVARCHAR (256) NOT NULL,
    [FileName]         NVARCHAR (256) NOT NULL,
    [Identity]         NVARCHAR (256) NOT NULL,
    [PrimaryPanelId]   INT            CONSTRAINT [DF_WebTemplates_PrimaryPanelId] DEFAULT ((-1)) NOT NULL,
    [Version]          INT            CONSTRAINT [DF_WebTemplate_Version] DEFAULT ((1)) NOT NULL,
    [VersionOf]        INT            CONSTRAINT [DF_WebTemplate_LatestVersion] DEFAULT ((1)) NOT NULL,
    [Content]          NVARCHAR (MAX) CONSTRAINT [DF_WebTemplate_Content] DEFAULT ('') NOT NULL,
    [DateModified]     DATETIME       NOT NULL,
    [ThemeId]          INT            CONSTRAINT [DF_WebTemplate_SkinId] DEFAULT ((-1)) NOT NULL,
    [Standalone]       INT            CONSTRAINT [DF__WebTempla__Standalone] DEFAULT ((0)) NOT NULL,
    [ParentId]         INT            CONSTRAINT [DF__WebTempla__ParentId] DEFAULT ((-1)) NOT NULL,
    [SkinId]           INT            CONSTRAINT [DF__WebTempla__SkinId] DEFAULT ((-1)) NOT NULL,
    [TemplateEngineId] INT            CONSTRAINT [DF__WebTempla__TemplateEngineId] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_WebTemplates] PRIMARY KEY CLUSTERED ([Id] ASC)
);



