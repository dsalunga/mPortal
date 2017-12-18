CREATE TABLE [dbo].[WebSite] (
    [SiteId]              INT            NOT NULL,
    [Name]                NVARCHAR (256) NOT NULL,
    [Rank]                INT            NOT NULL,
    [Active]              INT            NOT NULL,
    [Identity]            NVARCHAR (64)  NOT NULL,
    [Title]               NVARCHAR (256) NOT NULL,
    [ParentId]            INT            NOT NULL,
    [HomePageId]          INT            NOT NULL,
    [DefaultMasterPageId] INT            NOT NULL,
    [HostName]            NVARCHAR (256) NOT NULL,
    [Published]           INT            CONSTRAINT [DF_WebSites_Published] DEFAULT ((-1)) NOT NULL,
    [VersionOf]           INT            CONSTRAINT [DF_WebSites_VersionOfId] DEFAULT ((-1)) NOT NULL,
    [PublicAccess]        INT            CONSTRAINT [DF_WebSites_AuthenticationTypeId] DEFAULT ((128)) NOT NULL,
    [DateCreated]         DATETIME       CONSTRAINT [DF_WebSites_DateCreated] DEFAULT (getdate()) NOT NULL,
    [DateModified]        DATETIME       CONSTRAINT [DF_WebSites_DateModified] DEFAULT (getdate()) NOT NULL,
    [LoginPage]           NVARCHAR (250) CONSTRAINT [DF_WebSites_LoginPage] DEFAULT ('') NOT NULL,
    [AccessDeniedPage]    NVARCHAR (250) CONSTRAINT [DF_WebSites_AccessDeniedPage] DEFAULT ('') NOT NULL,
    [PageTitleFormat]     NVARCHAR (250) COLLATE Latin1_General_CI_AI CONSTRAINT [DF_WebSite_PageTitleFormat] DEFAULT ('') NOT NULL,
    [ManagementAccess]    INT            CONSTRAINT [DF_WebSite_ManagementAccess] DEFAULT ((0)) NOT NULL,
    [BaseAddress]         NVARCHAR (500) CONSTRAINT [DF_WebSite_BaseAddress] DEFAULT ('') NOT NULL,
    [ThemeId]             INT            CONSTRAINT [DF__WebSite__ThemeId] DEFAULT ((-1)) NOT NULL,
    [LocaleId]            INT            CONSTRAINT [DF__WebSite__LocaleId] DEFAULT ((-1)) NOT NULL,
    [SkinId]              INT            CONSTRAINT [DF__WebSite__SkinId] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_WebSites] PRIMARY KEY CLUSTERED ([SiteId] ASC)
);



