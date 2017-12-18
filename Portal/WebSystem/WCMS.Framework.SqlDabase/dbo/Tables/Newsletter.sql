CREATE TABLE [dbo].[Newsletter] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (500) NOT NULL,
    [Email]         NVARCHAR (50)  NOT NULL,
    [IPAddress]     NVARCHAR (50)  NOT NULL,
    [SubscribeDate] DATETIME       NOT NULL,
    [ObjectId]      INT            NOT NULL,
    [RecordId]      INT            NOT NULL,
    [SiteId]        INT            NOT NULL,
    [Gender]        INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

