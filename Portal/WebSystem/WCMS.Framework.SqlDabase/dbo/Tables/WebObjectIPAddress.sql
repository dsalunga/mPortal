CREATE TABLE [dbo].[WebObjectIPAddress] (
    [Id]        INT           NOT NULL,
    [ObjectId]  INT           NOT NULL,
    [RecordId]  INT           NOT NULL,
    [IPAddress] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_WebIPAddressPermission] PRIMARY KEY CLUSTERED ([Id] ASC)
);

