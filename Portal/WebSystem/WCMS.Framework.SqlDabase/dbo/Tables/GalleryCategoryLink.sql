CREATE TABLE [dbo].[GalleryCategoryLink] (
    [Id]         INT IDENTITY (1, 1) NOT NULL,
    [SiteId]     INT NOT NULL,
    [ObjectId]   INT NOT NULL,
    [RecordId]   INT NOT NULL,
    [CategoryId] INT NOT NULL,
    CONSTRAINT [PK_GalleryCategoryLocation] PRIMARY KEY CLUSTERED ([Id] ASC)
);

