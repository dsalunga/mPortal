CREATE TABLE [dbo].[RemoteLibrary] (
    [Id]                        INT            NOT NULL,
    [Name]                      NVARCHAR (300) COLLATE Latin1_General_CI_AI CONSTRAINT [DF_RemoteLibrary_Name] DEFAULT ('') NOT NULL,
    [SourceTypeId]              INT            CONSTRAINT [DF_RemoteLibrary_SourceTypeId] DEFAULT ((0)) NOT NULL,
    [BaseAddress]               NVARCHAR (500) COLLATE Latin1_General_CI_AI CONSTRAINT [DF_RemoteLibrary_BaseAddress] DEFAULT ('') NOT NULL,
    [UserName]                  NVARCHAR (50)  COLLATE Latin1_General_CI_AI CONSTRAINT [DF_RemoteLibrary_UserName] DEFAULT ('') NOT NULL,
    [Password]                  NVARCHAR (50)  COLLATE Latin1_General_CI_AI CONSTRAINT [DF_RemoteLibrary_Password] DEFAULT ('') NOT NULL,
    [LastIndexDate]             DATETIME       CONSTRAINT [DF_RemoteLibrary_LastIndexDate] DEFAULT (getdate()) NOT NULL,
    [Active]                    INT            CONSTRAINT [DF_RemoteLibrary_Active] DEFAULT ((1)) NOT NULL,
    [DisplayBaseAddress]        NVARCHAR (500) CONSTRAINT [DF_RemoteLibrary_DisplayBaseAddress] DEFAULT ('') NOT NULL,
    [DownloadCountSince]        DATETIME       CONSTRAINT [DF_RemoteLibrary_DownloadCountSince] DEFAULT (getdate()) NOT NULL,
    [FileCacheEnabled]          INT            CONSTRAINT [DF_RemoteLibrary_FileCacheEnabled] DEFAULT ((0)) NOT NULL,
    [FileCacheFolder]           NVARCHAR (500) CONSTRAINT [DF_RemoteLibrary_FileCacheFolder] DEFAULT ('') NOT NULL,
    [FileCacheMinDownloadCount] INT            CONSTRAINT [DF_RemoteLibrary_FileCacheMinDwldCount] DEFAULT ((-1)) NOT NULL,
    [FileCacheCeilingSize]      INT            CONSTRAINT [DF_RemoteLibrary_FileCacheCeilSize] DEFAULT ((-1)) NOT NULL,
    [FileCacheMaxSize]          INT            CONSTRAINT [DF_RemoteLibrary_FileCacheMaxSize] DEFAULT ((-1)) NOT NULL,
    [FileCacheMinDiskFreeMB]    INT            CONSTRAINT [DF_RemoteLibrary_FileCacheMinDiskFree] DEFAULT ((-1)) NOT NULL,
    [Size]                      BIGINT         DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_RemoteLibrary] PRIMARY KEY CLUSTERED ([Id] ASC)
);



