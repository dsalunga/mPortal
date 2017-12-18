CREATE TABLE [dbo].[CountryState] (
    [StateCode]   INT            NOT NULL,
    [StateName]   NVARCHAR (256) CONSTRAINT [DF_USState_StateName] DEFAULT ('') NOT NULL,
    [ZipCode]     NVARCHAR (64)  CONSTRAINT [DF_USState_ZipCode] DEFAULT ('') NOT NULL,
    [CountryCode] INT            CONSTRAINT [DF_USState_CountryCode] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_MISC_CountryState] PRIMARY KEY CLUSTERED ([StateCode] ASC)
);

