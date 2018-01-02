CREATE TABLE [dbo].[BibleTranslation_old] (
    [TranslationCode] INT            NOT NULL,
    [Name]            NVARCHAR (250) COLLATE Latin1_General_CI_AI NOT NULL,
    [LanguageCode]    NVARCHAR (50)  COLLATE Latin1_General_CI_AI NOT NULL,
    [CountryCode]     NVARCHAR (50)  COLLATE Latin1_General_CI_AI NOT NULL,
    CONSTRAINT [PK_Translation] PRIMARY KEY CLUSTERED ([TranslationCode] ASC)
);

