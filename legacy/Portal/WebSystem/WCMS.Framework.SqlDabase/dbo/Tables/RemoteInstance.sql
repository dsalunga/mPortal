CREATE TABLE [dbo].[RemoteInstance] (
    [Id]      INT            NOT NULL,
    [Name]    NVARCHAR (500) NOT NULL,
    [BaseUrl] NVARCHAR (500) NOT NULL,
    CONSTRAINT [PK_RemoteInstance] PRIMARY KEY CLUSTERED ([Id] ASC)
);

