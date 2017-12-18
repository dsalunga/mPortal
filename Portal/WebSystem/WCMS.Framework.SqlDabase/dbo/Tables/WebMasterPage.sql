CREATE TABLE [dbo].[WebMasterPage] (
    [MasterPageId]     INT            NOT NULL,
    [SiteId]           INT            NOT NULL,
    [TemplateId]       INT            NOT NULL,
    [Name]             NVARCHAR (256) NOT NULL,
    [OwnerPageId]      INT            CONSTRAINT [DF_WebMasterPages_OwnerPageId] DEFAULT ((-1)) NOT NULL,
    [PublicAccess]     INT            CONSTRAINT [DF_WebMasterPages_AuthenticationTypeId] DEFAULT ((1)) NOT NULL,
    [DateCreated]      DATETIME       CONSTRAINT [DF_WebMasterPages_DateCreated] DEFAULT (getdate()) NOT NULL,
    [DateModified]     DATETIME       CONSTRAINT [DF_WebMasterPages_DateModified] DEFAULT (getdate()) NOT NULL,
    [ManagementAccess] INT            CONSTRAINT [DF_WebMasterPage_ManagementAccess] DEFAULT ((0)) NOT NULL,
    [ThemeId]          INT            CONSTRAINT [DF_WebMasterPage_ThemeId] DEFAULT ((-1)) NOT NULL,
    [SkinId]           INT            CONSTRAINT [DF_WebMasterPage__SkinId] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_WebMasterPages] PRIMARY KEY CLUSTERED ([MasterPageId] ASC)
);



