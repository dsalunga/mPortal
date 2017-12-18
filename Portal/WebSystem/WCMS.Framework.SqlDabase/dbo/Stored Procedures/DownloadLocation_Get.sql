CREATE PROCEDURE [DownloadLocation_Get] 
(
		@PageType int,
		@SitePageItemID int,
		@InsertedOnly bit = 1,
		
		@Year int = null,
		@GroupByYear bit = null,
		@MaxRecords int = null
	)
AS
	SET NOCOUNT ON
	
	if(@InsertedOnly = 1)
		begin
			/* Inserted only */
			if(@Year is not null)
				begin
					SELECT     DL.DownloadLocationID, D.DownloadID, D.Name, D.FileDate, D.Filename, D.DateModified, D.Rank
					FROM         Download AS D INNER JOIN
					                      DownloadLocation AS DL ON D.DownloadID = DL.DownloadID
					WHERE     (YEAR(D.FileDate) = @Year) AND (DL.PageType = @PageType) AND (DL.SitePageItemID = @SitePageItemID)
					ORDER BY D.FileDate DESC, D.Rank, D.Name
				end
			else if(@GroupByYear is not null)
				begin
					SELECT     CAST(YEAR(D.FileDate) AS int) AS FileYear
					FROM         Download AS D INNER JOIN
					                      DownloadLocation AS DL ON D.DownloadID = DL.DownloadID
					WHERE     (DL.PageType = @PageType) AND (DL.SitePageItemID = @SitePageItemID)
					GROUP BY YEAR(D.FileDate)
					ORDER BY FileYear DESC
				end
			else if(@MaxRecords is not null)
				begin
					SELECT TOP(@MaxRecords)     DL.DownloadLocationID, D.DownloadID, D.Name, D.FileDate, D.Filename, D.DateModified, D.Rank
					FROM         Download AS D INNER JOIN
										  DownloadLocation AS DL ON D.DownloadID = DL.DownloadID
					WHERE     (DL.PageType = @PageType) AND (DL.SitePageItemID = @SitePageItemID)
					ORDER BY D.FileDate DESC, D.Rank, D.Name
				end
			else
				begin
					SELECT     DL.DownloadLocationID, D.DownloadID, D.Name, D.FileDate, D.Filename, D.DateModified, D.Rank
					FROM         Download AS D INNER JOIN
										  DownloadLocation AS DL ON D.DownloadID = DL.DownloadID
					WHERE     (DL.PageType = @PageType) AND (DL.SitePageItemID = @SitePageItemID)
					ORDER BY D.FileDate DESC, D.Rank, D.Name
				end
		end
	else
		begin
			SELECT     DownloadID, Name, FileDate, Filename, DateModified, Rank
			FROM         Download
			WHERE     (DownloadID NOT IN
									  (SELECT     DL.DownloadID
										FROM          Download AS D INNER JOIN
															   DownloadLocation AS DL ON D.DownloadID = DL.DownloadID
										WHERE      (DL.PageType = @PageType) AND (DL.SitePageItemID = @SitePageItemID)))
			ORDER BY FileDate DESC, Rank, Name
		end
				
	RETURN