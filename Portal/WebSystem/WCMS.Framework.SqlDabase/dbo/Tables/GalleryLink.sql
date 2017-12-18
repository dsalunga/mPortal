CREATE TABLE [dbo].[GalleryLink] (
    [Id]        INT IDENTITY (1, 1) NOT NULL,
    [SiteId]    INT NOT NULL,
    [ObjectId]  INT NOT NULL,
    [RecordId]  INT NOT NULL,
    [GalleryId] INT NOT NULL,
    CONSTRAINT [PK_GalleryLocation] PRIMARY KEY CLUSTERED ([Id] ASC)
);

