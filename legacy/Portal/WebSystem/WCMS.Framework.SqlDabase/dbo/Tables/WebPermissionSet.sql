CREATE TABLE [dbo].[WebPermissionSet] (
    [Id]           INT NOT NULL,
    [ObjectId]     INT NOT NULL,
    [RecordId]     INT CONSTRAINT [DF_WebPermissionSet_RecordId] DEFAULT ((-1)) NOT NULL,
    [PermissionId] INT NOT NULL,
    [Public]       INT CONSTRAINT [DF_WebPermissionSet_Public] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_WebPermissionSet] PRIMARY KEY CLUSTERED ([Id] ASC)
);

