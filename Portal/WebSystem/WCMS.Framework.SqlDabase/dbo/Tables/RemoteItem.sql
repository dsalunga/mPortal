CREATE TABLE [dbo].[RemoteItem] (
    [Id]                INT            NOT NULL,
    [LibraryId]         INT            CONSTRAINT [DF_RemoteItems_LibraryId] DEFAULT ((-1)) NOT NULL,
    [Name]              NVARCHAR (300) COLLATE Latin1_General_CI_AI CONSTRAINT [DF_RemoteItems_Name] DEFAULT ('') NOT NULL,
    [RelativePath]      NVARCHAR (500) COLLATE Latin1_General_CI_AI CONSTRAINT [DF_RemoteItems_RelativePath] DEFAULT ('') NOT NULL,
    [TypeId]            INT            CONSTRAINT [DF_RemoteItems_TypeId] DEFAULT ((0)) NOT NULL,
    [DateModified]      DATETIME       CONSTRAINT [DF_RemoteItems_DateModified] DEFAULT (getdate()) NOT NULL,
    [Size]              INT            CONSTRAINT [DF_RemoteItems_Size] DEFAULT ((0)) NOT NULL,
    [Content]           NTEXT          COLLATE Latin1_General_CI_AI CONSTRAINT [DF_RemoteItems_Content] DEFAULT ('') NOT NULL,
    [ParentId]          INT            CONSTRAINT [DF_RemoteItems_ParentId] DEFAULT ((-1)) NOT NULL,
    [DownloadCount]     INT            CONSTRAINT [DF_RemoteItems_DownloadCount] DEFAULT ((0)) NOT NULL,
    [DisplayName]       NVARCHAR (500) CONSTRAINT [DF_RemoteItems_DisplayName] DEFAULT ('') NOT NULL,
    [IndexDateModified] DATETIME       CONSTRAINT [DF_RemoteItems_IdxDateMdf] DEFAULT (getdate()) NOT NULL,
    [FileCacheEnabled]  INT            CONSTRAINT [DF_RemoteItem_FileCachedEnabled] DEFAULT ((-1)) NOT NULL,
    [Cached]            INT            CONSTRAINT [DF_RemoteItem_Cached] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_RemoteItems] PRIMARY KEY CLUSTERED ([Id] ASC)
);



