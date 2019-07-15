CREATE TABLE [dbo].[BibleReaderVersionAccess] (
    [Id]                 INT            NOT NULL,
    [BibleAccessId]      INT            NOT NULL,
    [BibleVersionId]     INT            CONSTRAINT [DF_BibleReaderVersionAccess_BibleVersionId] DEFAULT ((-1)) NOT NULL,
    [BibleVersionName]   NVARCHAR (250) CONSTRAINT [DF_BibleReaderVersionAccess_BibleVersionName] DEFAULT ('') NOT NULL,
    [LastAccessed]       DATETIME       CONSTRAINT [DF_BibleReaderVersionAccess_LastAccess] DEFAULT (getdate()) NOT NULL,
    [VersionAccessCount] INT            CONSTRAINT [DF_BibleReaderVersionAccess_VersionAccessCount] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_BibleReaderVersionAccess] PRIMARY KEY CLUSTERED ([Id] ASC)
);

