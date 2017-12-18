CREATE TABLE [dbo].[WebPageElement] (
    [PageElementId]         INT            NOT NULL,
    [ObjectId]              INT            CONSTRAINT [DF_WebPageElement_ObjectId_1] DEFAULT ((2)) NOT NULL,
    [RecordId]              INT            NOT NULL,
    [Name]                  NVARCHAR (250) NOT NULL,
    [TemplatePanelId]       INT            NOT NULL,
    [Rank]                  INT            NOT NULL,
    [PartControlTemplateId] INT            NOT NULL,
    [Active]                INT            NOT NULL,
    [UsePartTemplatePath]   INT            CONSTRAINT [DF_WebPageElement_UsePartTemplatePath_1] DEFAULT ((1)) NOT NULL,
    [PublicAccess]          INT            CONSTRAINT [DF_WebPageElement_PublicAccess] DEFAULT ((1)) NOT NULL,
    [DateModified]          DATETIME       CONSTRAINT [DF_WebPageElement_DateModified] DEFAULT (getdate()) NOT NULL,
    [ManagementAccess]      INT            CONSTRAINT [DF_WebPageElement_ManagementAccess] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_WebMasterPageItems] PRIMARY KEY CLUSTERED ([PageElementId] ASC)
);

