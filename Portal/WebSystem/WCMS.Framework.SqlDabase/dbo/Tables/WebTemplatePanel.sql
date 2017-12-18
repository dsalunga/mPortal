CREATE TABLE [dbo].[WebTemplatePanel] (
    [TemplatePanelId] INT            NOT NULL,
    [Name]            NVARCHAR (256) NOT NULL,
    [TemplateId]      INT            CONSTRAINT [DF_WebTemplatePanels_TemplateId] DEFAULT ((-1)) NOT NULL,
    [PanelName]       NVARCHAR (256) NOT NULL,
    [Rank]            INT            CONSTRAINT [DF_WebTemplatePanel_Rank] DEFAULT ((0)) NOT NULL,
    [ObjectId]        INT            CONSTRAINT [DF__WebTempla__ObjectId] DEFAULT ((-1)) NOT NULL,
    [RecordId]        INT            CONSTRAINT [DF__WebTempla__RecordId] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_WebTemplatePanels] PRIMARY KEY CLUSTERED ([TemplatePanelId] ASC)
);



