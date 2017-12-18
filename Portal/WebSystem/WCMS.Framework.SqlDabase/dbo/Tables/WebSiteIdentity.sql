CREATE TABLE [dbo].[WebSiteIdentity] (
    [Id]          INT            NOT NULL,
    [SiteId]      INT            CONSTRAINT [DF_WebSiteIdentity_SiteId] DEFAULT ((-1)) NOT NULL,
    [HostName]    NVARCHAR (256) CONSTRAINT [DF_WebSiteIdentity_HostName] DEFAULT ('') NOT NULL,
    [UrlPath]     NVARCHAR (256) CONSTRAINT [DF_WebSiteIdentity_UrlPath] DEFAULT ('') NOT NULL,
    [Port]        INT            CONSTRAINT [DF_WebSiteIdentity_Port] DEFAULT ((80)) NOT NULL,
    [IPAddress]   NVARCHAR (50)  CONSTRAINT [DF_WebSiteIdentity_IPAddress] DEFAULT ('') NOT NULL,
    [RedirectUrl] NVARCHAR (500) CONSTRAINT [DF__WebSiteId__RedirectUrl] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_WebSiteIdentity] PRIMARY KEY CLUSTERED ([Id] ASC)
);



