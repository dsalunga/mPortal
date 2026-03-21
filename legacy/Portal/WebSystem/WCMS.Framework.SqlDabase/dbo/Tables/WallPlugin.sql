CREATE TABLE [dbo].[WallPlugin] (
    [Id]          INT             NOT NULL,
    [Name]        NVARCHAR (2000) NOT NULL,
    [EventTypeId] INT             NOT NULL,
    [FileName]    NVARCHAR (2000) NOT NULL,
    [TypeName]    NVARCHAR (2000) NOT NULL,
    CONSTRAINT [PK_WallPlugin] PRIMARY KEY CLUSTERED ([Id] ASC)
);

