CREATE TABLE [dbo].[WebGroup] (
    [Id]           INT            NOT NULL,
    [Name]         NVARCHAR (250) NOT NULL,
    [ParentId]     INT            CONSTRAINT [DF_WebGroup_ParentId] DEFAULT ((-1)) NOT NULL,
    [IsSystem]     INT            CONSTRAINT [DF_WebGroup_IsSystem] DEFAULT ((0)) NOT NULL,
    [DateModified] DATETIME       CONSTRAINT [DF_WebGroup_DateModified] DEFAULT (getdate()) NOT NULL,
    [OwnerId]      INT            CONSTRAINT [DF_WebGroup_OwnerId] DEFAULT ((-1)) NOT NULL,
    [JoinApproval] INT            CONSTRAINT [DF_WebGroup_JoinApproval] DEFAULT ((0)) NOT NULL,
    [JoinAlert]    INT            CONSTRAINT [DF_WebGroup_JoinAlert] DEFAULT ((0)) NOT NULL,
    [PageUrl]      NVARCHAR (250) CONSTRAINT [DF_WebGroup_PageUrl] DEFAULT ('') NOT NULL,
    [PageId]       INT            CONSTRAINT [DF_WebGroup_PageId] DEFAULT ((-1)) NOT NULL,
    [Description]  NVARCHAR (MAX) CONSTRAINT [DF_WebGroup_Description] DEFAULT ('') NOT NULL,
    [Managers]     NVARCHAR (MAX) CONSTRAINT [DF__WebGroup__Managers] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_WebGroups] PRIMARY KEY CLUSTERED ([Id] ASC)
);



