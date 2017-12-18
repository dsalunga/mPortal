CREATE TABLE [dbo].[WebAddress] (
    [Id]                INT            NOT NULL,
    [AddressLine1]      NVARCHAR (250) COLLATE Latin1_General_CI_AI CONSTRAINT [DF_Table1_HomeAddressLine1] DEFAULT ('') NOT NULL,
    [AddressLine2]      NVARCHAR (250) COLLATE Latin1_General_CI_AI CONSTRAINT [DF_WebAddress_AddressLine2] DEFAULT ('') NOT NULL,
    [CityTown]          NVARCHAR (50)  COLLATE Latin1_General_CI_AI CONSTRAINT [DF_WebAddress_CityTown] DEFAULT ('') NOT NULL,
    [StateProvince]     NVARCHAR (50)  COLLATE Latin1_General_CI_AI CONSTRAINT [DF_WebAddress_StateProvince] DEFAULT ('') NOT NULL,
    [StateProvinceCode] INT            CONSTRAINT [DF_Table1_HomeAddressStateCode] DEFAULT ((-1)) NOT NULL,
    [CountryCode]       INT            CONSTRAINT [DF_Table1_HomeAddressCountryCode] DEFAULT ((-1)) NOT NULL,
    [ZipCode]           NVARCHAR (50)  COLLATE Latin1_General_CI_AI CONSTRAINT [DF_Table1_HomeAddressZipCode] DEFAULT ('') NOT NULL,
    [PhoneNumber]       NVARCHAR (50)  COLLATE Latin1_General_CI_AI CONSTRAINT [DF_WebAddress_PhoneNumber] DEFAULT ('') NOT NULL,
    [ObjectId]          INT            CONSTRAINT [DF_WebAddress_UserId] DEFAULT ((-1)) NOT NULL,
    [RecordId]          INT            CONSTRAINT [DF_WebAddress_RecordId] DEFAULT ((-1)) NOT NULL,
    [Tag]               NVARCHAR (50)  COLLATE Latin1_General_CI_AI NOT NULL,
    [LastUpdated]       DATETIME       CONSTRAINT [DF_WebAddress_LastUpdated] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_WebAddress] PRIMARY KEY CLUSTERED ([Id] ASC)
);

