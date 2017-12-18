CREATE PROCEDURE [GalleryLink_GetTypeId]
(
		@ObjectId int,
		@RecordId int,
		@InsertedOnly bit = 1,
		@CategoryId int = null
	)
AS
	SET NOCOUNT ON
	
	if(@CategoryId is not null)
		begin
			if(@InsertedOnly = 1)
				begin
					SELECT     GL.Id, G.Caption, G.DateCreated, G.IsActive, C.Title, G.GalleryId
					FROM         Gallery AS G INNER JOIN
					                      GalleryLink AS GL ON G.GalleryId = GL.GalleryId LEFT OUTER JOIN
					                      GalleryCategory AS C ON G.CategoryId = C.CategoryId
					WHERE     (GL.ObjectId = @ObjectId) AND (GL.RecordId = @RecordId) AND (C.CategoryId = @CategoryId)
				end
			else
				begin
					SELECT     G2.GalleryId, G2.Caption, G2.DateCreated, G2.IsActive, C.Title
					FROM         Gallery AS G2 LEFT OUTER JOIN
					                      GalleryCategory AS C ON G2.CategoryId = C.CategoryId
					WHERE     (G2.GalleryId NOT IN
					                          (SELECT     GL.GalleryId
					                            FROM          Gallery AS G INNER JOIN
					                                                   GalleryLink AS GL ON G.GalleryId = GL.GalleryId
					                            WHERE      (GL.ObjectId = @ObjectId) AND (GL.RecordId = @RecordId))) AND (C.CategoryId = @CategoryId)
				end
		end
	else
		begin
			if(@InsertedOnly = 1)
				begin
					SELECT     GL.Id, G.Caption, G.DateCreated, G.IsActive, C.Title, G.GalleryId
					FROM         Gallery AS G INNER JOIN
										  GalleryLink AS GL ON G.GalleryId = GL.GalleryId LEFT OUTER JOIN
										  GalleryCategory AS C ON G.CategoryId = C.CategoryId
					WHERE     (GL.ObjectId = @ObjectId) AND (GL.RecordId = @RecordId)
				end
			else
				begin
					SELECT     G2.GalleryId, G2.Caption, G2.DateCreated, G2.IsActive, C.Title
					FROM         Gallery AS G2 LEFT OUTER JOIN
										  GalleryCategory AS C ON G2.CategoryId = C.CategoryId
					WHERE     (G2.GalleryId NOT IN
											  (SELECT     GL.GalleryId
												FROM          Gallery AS G INNER JOIN
																	   GalleryLink AS GL ON G.GalleryId = GL.GalleryId
												WHERE      (GL.ObjectId = @ObjectId) AND (GL.RecordId = @RecordId)))
				end
		end
	RETURN