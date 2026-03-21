CREATE TABLE [dbo].[MenuItem] (
    [Id]              INT            NOT NULL,
    [NavigateUrl]     NVARCHAR (256) NOT NULL,
    [Text]            NVARCHAR (256) NOT NULL,
    [Target]          NVARCHAR (256) NOT NULL,
    [ParentId]        INT            CONSTRAINT [DF_MenuItem_ParentId] DEFAULT ((-1)) NOT NULL,
    [MenuId]          INT            NOT NULL,
    [IsActive]        INT            NOT NULL,
    [Rank]            INT            NOT NULL,
    [PageId]          INT            CONSTRAINT [DF_MenuItem_PageId] DEFAULT ((-1)) NOT NULL,
    [Type]            INT            CONSTRAINT [DF_MenuItem_Type] DEFAULT ((1)) NOT NULL,
    [CheckPermission] INT            CONSTRAINT [DF_MenuItem_CheckPermission] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MenuItems] PRIMARY KEY CLUSTERED ([Id] ASC)
);

