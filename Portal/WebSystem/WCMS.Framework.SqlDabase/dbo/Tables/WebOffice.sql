CREATE TABLE [dbo].[WebOffice] (
    [OfficeId]      INT            NOT NULL,
    [Name]          NVARCHAR (250) COLLATE Latin1_General_CI_AI CONSTRAINT [DF_WebOffice_Name] DEFAULT ('') NOT NULL,
    [ParentId]      INT            CONSTRAINT [DF_WebOffice_ParentId] DEFAULT ((-1)) NOT NULL,
    [AddressLine1]  NVARCHAR (250) COLLATE Latin1_General_CI_AI CONSTRAINT [DF_WebOffice_AddressLine1] DEFAULT ('') NOT NULL,
    [MobileNumber]  NVARCHAR (50)  COLLATE Latin1_General_CI_AI CONSTRAINT [DF_WebOffice_MobileNumber] DEFAULT ('') NOT NULL,
    [PhoneNumber]   NVARCHAR (50)  COLLATE Latin1_General_CI_AI CONSTRAINT [DF_WebOffice_PhoneNumber] DEFAULT ('') NOT NULL,
    [EmailAddress]  NVARCHAR (50)  COLLATE Latin1_General_CI_AI CONSTRAINT [DF_WebOffice_EmailAddress] DEFAULT ('') NOT NULL,
    [ContactPerson] NVARCHAR (250) COLLATE Latin1_General_CI_AI CONSTRAINT [DF_WebOffice_ContactPersonId] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_WebOffice] PRIMARY KEY CLUSTERED ([OfficeId] ASC)
);

