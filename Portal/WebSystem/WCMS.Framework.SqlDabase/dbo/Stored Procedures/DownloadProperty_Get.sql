CREATE PROCEDURE [DownloadProperty_Get] 
(
		@SitePageItemID int,
		@PageType int
	)
AS
	SET NOCOUNT ON
	
	SELECT     TOP (1) DownloadPropertyID, InitialControl, Columns, Rows, MaxRecords, ForceDownload
	FROM         DownloadProperty
	WHERE     (PageType = @PageType) AND (SitePageItemID = @SitePageItemID)
	
	RETURN