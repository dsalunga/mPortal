CREATE TABLE [dbo].[Menu] (
    [Id]              INT            NOT NULL,
    [Name]            NVARCHAR (256) NOT NULL,
    [IsActive]        INT            NOT NULL,
    [DateCreated]     DATETIME       NOT NULL,
    [SiteId]          INT            NOT NULL,
    [UserId]          INT            NOT NULL,
    [PageId]          INT            CONSTRAINT [DF_Menu_RefPageId] DEFAULT ((-2)) NOT NULL,
    [IncludeChildren] INT            CONSTRAINT [DF_Menu_IncludeChildren] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Menus] PRIMARY KEY CLUSTERED ([Id] ASC)
);

