CREATE TABLE [dbo].[WebContent] (
    [ContentId]       INT            NOT NULL,
    [Title]           NVARCHAR (256) NOT NULL,
    [Content]         NVARCHAR (MAX) NOT NULL,
    [VersionOf]       INT            CONSTRAINT [DF_WebContents_VersionOfId] DEFAULT ((-1)) NOT NULL,
    [VersionNo]       INT            CONSTRAINT [DF_WebContents_VersionNo] DEFAULT ((-1)) NOT NULL,
    [DirectoryId]     INT            CONSTRAINT [DF_WebContents_DirectoryId] DEFAULT ((-1)) NOT NULL,
    [DateModified]    DATETIME       CONSTRAINT [DF_WebContent_DateModified] DEFAULT (getdate()) NOT NULL,
    [ContentTypeId]   INT            CONSTRAINT [DF_WebContent_ContentTypeId] DEFAULT ((4)) NOT NULL,
    [Active]          INT            CONSTRAINT [DF_WebContent_Active] DEFAULT ((1)) NOT NULL,
    [SiteId]          INT            CONSTRAINT [DF_WebContent_SiteId] DEFAULT ((-1)) NOT NULL,
    [EditorSensitive] INT            CONSTRAINT [DF__WebConten__EditorSensitive] DEFAULT ((0)) NOT NULL,
    [ActiveContent]   INT            CONSTRAINT [DF_WebConten_ActiveContent] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_WebContents] PRIMARY KEY CLUSTERED ([ContentId] ASC)
);



