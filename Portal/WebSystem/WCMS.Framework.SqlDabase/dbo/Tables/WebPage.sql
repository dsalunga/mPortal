CREATE TABLE [dbo].[WebPage] (
    [PageId]                INT            NOT NULL,
    [Name]                  NVARCHAR (255) NOT NULL,
    [SiteId]                INT            NOT NULL,
    [Rank]                  INT            NOT NULL,
    [Active]                INT            NOT NULL,
    [Identity]              NVARCHAR (255) NOT NULL,
    [ParentId]              INT            NOT NULL,
    [Title]                 NVARCHAR (255) NOT NULL,
    [MasterPageId]          INT            NOT NULL,
    [PartControlTemplateId] INT            CONSTRAINT [DF_WebPages_PartControlTemplateId] DEFAULT ((-1)) NOT NULL,
    [Published]             INT            CONSTRAINT [DF_WebPages_Published] DEFAULT ((-1)) NOT NULL,
    [VersionOfId]           INT            CONSTRAINT [DF_WebPages_VersionOfId] DEFAULT ((-1)) NOT NULL,
    [PublicAccess]          INT            CONSTRAINT [DF_WebPages_AuthMethodId] DEFAULT ((128)) NOT NULL,
    [DateCreated]           DATETIME       CONSTRAINT [DF_WebPages_DateCreated] DEFAULT (getdate()) NOT NULL,
    [DateModified]          DATETIME       CONSTRAINT [DF_WebPages_DateModified] DEFAULT (getdate()) NOT NULL,
    [PageType]              INT            CONSTRAINT [DF_WebPages_PageType] DEFAULT ((0)) NOT NULL,
    [UsePartTemplatePath]   INT            CONSTRAINT [DF_WebPages_UseTemplatePath] DEFAULT ((1)) NOT NULL,
    [ManagementAccess]      INT            CONSTRAINT [DF_WebPage_ManagementAccess] DEFAULT ((0)) NOT NULL,
    [ThemeId]               INT            CONSTRAINT [DF_WebPage_ThemeId] DEFAULT ((-1)) NOT NULL,
    [LocaleId]              INT            CONSTRAINT [DF__WebPage__LocaleId] DEFAULT ((-1)) NOT NULL,
    [SkinId]                INT            CONSTRAINT [DF__WebPage__SkinId] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_WebPages] PRIMARY KEY CLUSTERED ([PageId] ASC)
);



