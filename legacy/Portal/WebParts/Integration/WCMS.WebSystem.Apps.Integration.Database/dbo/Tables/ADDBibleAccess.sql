CREATE TABLE [dbo].[BibleReaderAccess] (
    [Id]             INT      NOT NULL,
    [UserId]         INT      NOT NULL,
    [AppAccessCount] INT      CONSTRAINT [DF_BibleReaderAccess_AppAccessCount] DEFAULT ((-1)) NOT NULL,
    [LastAccessed]   DATETIME CONSTRAINT [DF_BibleReaderAccess_LastAccess] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_BibleReaderAccess] PRIMARY KEY CLUSTERED ([Id] ASC)
);

