CREATE TABLE [dbo].[WebPermission] (
    [Id]       INT            NOT NULL,
    [Name]     NVARCHAR (250) NOT NULL,
    [IsSystem] INT            CONSTRAINT [DF_WebPermission_IsSystem] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_WebPermissions] PRIMARY KEY CLUSTERED ([Id] ASC)
);

