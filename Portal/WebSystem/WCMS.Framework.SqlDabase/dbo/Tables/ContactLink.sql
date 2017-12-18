CREATE TABLE [dbo].[ContactLink] (
    [Id]        INT NOT NULL,
    [RecordId]  INT NOT NULL,
    [ObjectId]  INT NOT NULL,
    [ContactId] INT NOT NULL,
    [Mode]      INT CONSTRAINT [DF_ContactLink_Mode] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_SiteProperties_1] PRIMARY KEY CLUSTERED ([Id] ASC)
);

