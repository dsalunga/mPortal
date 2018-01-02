CREATE TABLE [dbo].[BibleVersion] (
    [BibleTableName]  NVARCHAR (500) NOT NULL,
    [Name]            NVARCHAR (500) NOT NULL,
    [BookNameCode]    INT            NOT NULL,
    [OldAndNew]       INT            NOT NULL,
    [LanguageType]    INT            NOT NULL,
    [TranslationType] INT            NOT NULL,
    [Copyright]       INT            NOT NULL,
    [Id]              INT            NOT NULL,
    [Active]          INT            NOT NULL,
    [ShortName]       NVARCHAR (50)  CONSTRAINT [DF_BibleVersion_VersionCode] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_BibleVersion] PRIMARY KEY CLUSTERED ([Id] ASC)
);

