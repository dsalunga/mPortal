CREATE TABLE [dbo].[WebPartControlTemplate] (
    [PartControlTemplateId] INT            NOT NULL,
    [PartControlId]         INT            NOT NULL,
    [Name]                  NVARCHAR (256) NOT NULL,
    [FileName]              NVARCHAR (256) NOT NULL,
    [Identity]              NVARCHAR (256) NOT NULL,
    [Path]                  NVARCHAR (250) CONSTRAINT [DF_WebPartControlTemplates_CompletePath] DEFAULT ('') NOT NULL,
    [Standalone]            INT            CONSTRAINT [DF__WebPartControlTemplate__Standalone] DEFAULT ((0)) NOT NULL,
    [TemplateEngineId]      INT            CONSTRAINT [DF_WebPartControlTemplate_EngineId] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_WebPartControlTemplates] PRIMARY KEY CLUSTERED ([PartControlTemplateId] ASC)
);



