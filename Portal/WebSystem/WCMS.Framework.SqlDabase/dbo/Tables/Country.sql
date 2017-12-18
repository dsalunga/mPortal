CREATE TABLE [dbo].[Country] (
    [CountryCode]   INT            NOT NULL,
    [CountryName]   NVARCHAR (256) CONSTRAINT [DF_Country_CountryName] DEFAULT ('') NOT NULL,
    [RegionCode]    INT            CONSTRAINT [DF_Country_RegionCode] DEFAULT ((-1)) NOT NULL,
    [Description]   NVARCHAR (500) COLLATE Latin1_General_CI_AI CONSTRAINT [DF_Country_Description] DEFAULT ('') NOT NULL,
    [ISOCode]       NVARCHAR (50)  COLLATE Latin1_General_CI_AI CONSTRAINT [DF_Country_ISOCode] DEFAULT ('') NOT NULL,
    [DialingCode]   INT            CONSTRAINT [DF_Country_DialingCode] DEFAULT ((-1)) NOT NULL,
    [MaxPhoneDigit] INT            CONSTRAINT [DF_Country_MaxPhoneDigit] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_MISC_Countries] PRIMARY KEY CLUSTERED ([CountryCode] ASC)
);

