CREATE TABLE [dbo].[Registration] (
    [Id]            INT             NOT NULL,
    [EntryDate]     DATETIME        CONSTRAINT [DF_Registration_EntryDate] DEFAULT (getdate()) NOT NULL,
    [Country]       NVARCHAR (250)  COLLATE Latin1_General_CI_AI CONSTRAINT [DF_Registration_Country] DEFAULT ('') NOT NULL,
    [Locale]        NVARCHAR (500)  COLLATE Latin1_General_CI_AI CONSTRAINT [DF_Registration_Locale] DEFAULT ('') NOT NULL,
    [ExternalId]      NVARCHAR (50)   COLLATE Latin1_General_CI_AI CONSTRAINT [DF_Registration_ExternalId] DEFAULT ('') NOT NULL,
    [Name]          NVARCHAR (100)  COLLATE Latin1_General_CI_AI CONSTRAINT [DF_Registration_Name] DEFAULT ('') NOT NULL,
    [Designation]   NVARCHAR (250)  COLLATE Latin1_General_CI_AI CONSTRAINT [DF_Registration_Designation] DEFAULT ('') NOT NULL,
    [ArrivalDate]   DATETIME        NOT NULL,
    [Airline]       NVARCHAR (250)  COLLATE Latin1_General_CI_AI CONSTRAINT [DF_Registration_Airline] DEFAULT ('') NOT NULL,
    [FlightNo]      NVARCHAR (250)  COLLATE Latin1_General_CI_AI CONSTRAINT [DF_Registration_FlightNo] DEFAULT ('') NOT NULL,
    [DepartureDate] DATETIME        NOT NULL,
    [Address]       NVARCHAR (2500) COLLATE Latin1_General_CI_AI CONSTRAINT [DF_Registration_Suggestion] DEFAULT ('') NOT NULL,
    [Age]           INT             CONSTRAINT [DF_Registration_Age] DEFAULT ((-1)) NOT NULL,
    [Gender]        NVARCHAR (50)   COLLATE Latin1_General_CI_AI CONSTRAINT [DF_Registration_Gender] DEFAULT ('') NOT NULL,
    [PlaceType]     NVARCHAR (250)  COLLATE Latin1_General_CI_AI CONSTRAINT [DF_Registration_PlaceType] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_Registration] PRIMARY KEY CLUSTERED ([Id] ASC)
);

