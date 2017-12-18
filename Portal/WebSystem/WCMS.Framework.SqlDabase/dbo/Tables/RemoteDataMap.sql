CREATE TABLE [dbo].[RemoteDataMap] (
    [Id]         INT NOT NULL,
    [InstanceId] INT NOT NULL,
    [ObjectId]   INT NOT NULL,
    [LocalId]    INT NOT NULL,
    [RemoteId]   INT NOT NULL,
    CONSTRAINT [PK_RemoteDataMap] PRIMARY KEY CLUSTERED ([Id] ASC)
);

