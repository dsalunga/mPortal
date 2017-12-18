CREATE TABLE [dbo].[WebApplication] (
    [Id]          INT            NOT NULL,
    [Name]        NVARCHAR (500) NOT NULL,
    [AppKey]      NVARCHAR (500) CONSTRAINT [DF_WebApplication_AppKey] DEFAULT ('') NOT NULL,
    [IpAddresses] NVARCHAR (50)  CONSTRAINT [DF_WebApplication_IpAddresses] DEFAULT ('') NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

