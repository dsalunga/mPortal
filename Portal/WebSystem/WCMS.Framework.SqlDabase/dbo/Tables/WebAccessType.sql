CREATE TABLE [dbo].[WebAccessType] (
    [AccessTypeId] INT            NOT NULL,
    [Name]         NVARCHAR (250) NOT NULL,
    CONSTRAINT [PK_WebAccessTypes] PRIMARY KEY CLUSTERED ([AccessTypeId] ASC)
);

