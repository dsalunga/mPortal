CREATE TABLE [dbo].[MChapter] (
    [Id]              INT             NOT NULL,
    [Name]            NVARCHAR (500)  COLLATE Latin1_General_CI_AI CONSTRAINT [DF_MChapter_Name] DEFAULT ('') NOT NULL,
    [ParentId]        INT             CONSTRAINT [DF_MChapter_ParentId] DEFAULT ((-1)) NOT NULL,
    [Address]         NVARCHAR (1500) COLLATE Latin1_General_CI_AI CONSTRAINT [DF_WebOffice_Address] DEFAULT ('') NOT NULL,
    [ChapterType]     INT             CONSTRAINT [DF_MChapter_ChapterType] DEFAULT ((0)) NOT NULL,
    [Latitude]        FLOAT (53)      CONSTRAINT [DF_MChapter_Latitude] DEFAULT ((0)) NOT NULL,
    [Longitude]       FLOAT (53)      CONSTRAINT [DF_MChapter_Longitude] DEFAULT ((0)) NOT NULL,
    [AccessType]      INT             CONSTRAINT [DF_MChapter_AccessType] DEFAULT ((0)) NOT NULL,
    [CountryCode]     INT             CONSTRAINT [DF_MChapter_CountryCode] DEFAULT ((0)) NOT NULL,
    [Email]           NVARCHAR (500)  CONSTRAINT [DF_MChapter_Email] DEFAULT ('') NOT NULL,
    [Mobile]          NVARCHAR (500)  CONSTRAINT [DF_MChapter_Mobile] DEFAULT ('') NOT NULL,
    [Telephone]       NVARCHAR (500)  CONSTRAINT [DF_MChapter_Telephone] DEFAULT ('') NOT NULL,
    [ServiceSchedule] NVARCHAR (1500) CONSTRAINT [DF_MChapter_ServiceSchedule] DEFAULT ('') NOT NULL,
    [MoreInfo]        NVARCHAR (MAX)  CONSTRAINT [DF_MChapter_MoreInfo] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_MChapter] PRIMARY KEY CLUSTERED ([Id] ASC)
);

