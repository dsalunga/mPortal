CREATE TABLE [dbo].[WebObjectSecurityPermission] (
    [Id]               INT NOT NULL,
    [ObjectSecurityId] INT NOT NULL,
    [PermissionId]     INT NOT NULL,
    [Allow]            INT CONSTRAINT [DF_WebObjectSecurityPermission_Allow] DEFAULT ((1)) NOT NULL,
    [Deny]             INT CONSTRAINT [DF_WebObjectSecurityPermission_Deny] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_WebObjectSecurityPermission] PRIMARY KEY CLUSTERED ([Id] ASC)
);

