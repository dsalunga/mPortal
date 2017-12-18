CREATE TABLE [dbo].[FileIdentity] (
    [Id]        INT             NOT NULL,
    [ObjectId]  INT             CONSTRAINT [DF_FileIdentity_ObjectId] DEFAULT ((-1)) NOT NULL,
    [RecordId]  INT             CONSTRAINT [DF_FileIdentity_RecordId] DEFAULT ((-1)) NOT NULL,
    [LibraryId] INT             CONSTRAINT [DF_FileIdentity_LibraryId] DEFAULT ((-1)) NOT NULL,
    [FilePath]  NVARCHAR (4000) COLLATE Latin1_General_CI_AI CONSTRAINT [DF_FileIdentity_FilePath] DEFAULT ('') NOT NULL,
    [Name]      NVARCHAR (500)  CONSTRAINT [DF_FileIdentity_Name] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_FileIdentity] PRIMARY KEY CLUSTERED ([Id] ASC)
);

