CREATE TABLE [dbo].[WebRegistry] (
    [RegistryId] INT            NOT NULL,
    [Key]        NVARCHAR (255) NOT NULL,
    [Value]      NTEXT          NULL,
    [ParentId]   INT            NOT NULL,
    [StageId]    INT            CONSTRAINT [DF_WebRegistry_StageId] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_WebRegistry] PRIMARY KEY CLUSTERED ([RegistryId] ASC)
);

