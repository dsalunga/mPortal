CREATE TABLE [dbo].[WebRole] (
    [Id]       INT            NOT NULL,
    [Name]     NVARCHAR (250) NOT NULL,
    [IsSystem] INT            CONSTRAINT [DF_WebRoles_IsSystem] DEFAULT ((0)) NOT NULL,
    [ParentId] INT            CONSTRAINT [DF_WebRoles_ParentId] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_WebRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);

