CREATE TABLE [dbo].[Gallery] (
    [GalleryID]   INT            IDENTITY (1, 1) NOT NULL,
    [Caption]     NVARCHAR (256) NULL,
    [Thumbnail]   NVARCHAR (256) NULL,
    [ImageURL]    NVARCHAR (256) NULL,
    [DateCreated] DATETIME       NULL,
    [SiteID]      INT            NULL,
    [CategoryID]  INT            NULL,
    [IsActive]    BIT            NULL,
    CONSTRAINT [PK_Gallery] PRIMARY KEY CLUSTERED ([GalleryID] ASC)
);

