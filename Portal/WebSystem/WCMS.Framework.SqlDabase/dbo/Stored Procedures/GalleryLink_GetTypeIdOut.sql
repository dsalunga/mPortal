CREATE PROCEDURE [GalleryLink_GetTypeIdOut]
(
		@ObjectId int,
		@RecordId int,
		@SiteId int = null
	)
AS
	SET NOCOUNT ON
	IF(@SiteId is null)
		BEGIN
			SELECT     GalleryId, Caption, DateCreated, IsActive
			FROM         Gallery
			WHERE  GalleryId NOT IN
									  (SELECT     GL.GalleryId
										FROM          Gallery G INNER JOIN
															   GalleryLink GL ON G.GalleryId = GL.GalleryId
										WHERE      (GL.ObjectId = @ObjectId) AND (GL.RecordId = @RecordId))
		END
	ELSE
		BEGIN
			SELECT     GalleryId, Caption, DateCreated, IsActive
			FROM         Gallery
			WHERE    GalleryId NOT IN
									  (SELECT     GL.GalleryId
										FROM          Gallery G INNER JOIN
															   GalleryLink GL ON G.GalleryId = GL.GalleryId
										WHERE      (GL.ObjectId = @ObjectId) AND (GL.RecordId = @RecordId)) AND (SiteId = @SiteId)
		END
	
	RETURN