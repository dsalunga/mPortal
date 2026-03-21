CREATE TABLE [dbo].[GalleryObject] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [ObjectId]         INT            NOT NULL,
    [RecordId]         INT            NOT NULL,
    [InitialControl]   NVARCHAR (256) NOT NULL,
    [ThumbColumns]     INT            NOT NULL,
    [ThumbRows]        INT            NOT NULL,
    [AlbumColumns]     INT            NOT NULL,
    [AlbumCellPadding] INT            NOT NULL,
    [MaxPhotoWidth]    INT            CONSTRAINT [DF_GalleryProperty_MaxPhotoWidth] DEFAULT ((700)) NOT NULL,
    CONSTRAINT [PK_SiteProperty] PRIMARY KEY CLUSTERED ([Id] ASC)
);

