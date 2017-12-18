CREATE TABLE [dbo].[Sportsfest] (
    [Id]          INT             NOT NULL,
    [MemberId]    INT             CONSTRAINT [DF_Sportsfest_MemberId] DEFAULT ((-1)) NOT NULL,
    [Name]        NVARCHAR (100)  COLLATE Latin1_General_CI_AI NOT NULL,
    [GroupColor]  NVARCHAR (50)   COLLATE Latin1_General_CI_AI NOT NULL,
    [Age]         INT             NOT NULL,
    [Mobile]      NVARCHAR (50)   COLLATE Latin1_General_CI_AI NOT NULL,
    [EntryDate]   DATETIME        CONSTRAINT [DF_Sportsfest_EntryDate] DEFAULT (getdate()) NOT NULL,
    [Sports]      NVARCHAR (50)   COLLATE Latin1_General_CI_AI NOT NULL,
    [Locale]      NVARCHAR (500)  COLLATE Latin1_General_CI_AI CONSTRAINT [DF_Sportsfest_Locale] DEFAULT ('') NOT NULL,
    [Suggestion]  NVARCHAR (2500) COLLATE Latin1_General_CI_AI CONSTRAINT [DF_Sportsfest_Suggestion] DEFAULT ('') NOT NULL,
    [CountryCode] INT             CONSTRAINT [DF_Sportsfest_CountryCode] DEFAULT ((-1)) NOT NULL,
    [ShirtSize]   NVARCHAR (50)   COLLATE Latin1_General_CI_AI CONSTRAINT [DF_Sportsfest_ShirtSize] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_Sportsfest] PRIMARY KEY CLUSTERED ([Id] ASC)
);

