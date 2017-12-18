CREATE PROCEDURE [DownloadProperty_Set] 
	(
		@SitePageItemID int,
		@PageType int,
	
		@InitialControl nvarchar(255) = null,
		@Columns int = null,
		@Rows int = null,
		@MaxRecords int = null,
		@ForceDownload bit = null
	)
AS
	SET NOCOUNT ON
	DECLARE @DownloadPropertyID int
	
	SET @DownloadPropertyID = (SELECT     TOP (1) DownloadPropertyID
	                          FROM         DownloadProperty
	                          WHERE     (PageType = @PageType) AND (SitePageItemID = @SitePageItemID))
	
	if(@DownloadPropertyID is null)
		begin
			/* INSERT */
			INSERT INTO DownloadProperty
			                      (SitePageItemID, PageType, InitialControl, Columns, Rows, MaxRecords, ForceDownload)
			VALUES     (@SitePageItemID,@PageType,@InitialControl,@Columns,@Rows,@MaxRecords,@ForceDownload)
		end
	else
		begin
			/* UPDATE */
			UPDATE    DownloadProperty
			SET              InitialControl = @InitialControl, Columns = @Columns, Rows = @Rows, MaxRecords = @MaxRecords, ForceDownload = @ForceDownload
			WHERE     (DownloadPropertyID = @DownloadPropertyID)
		end
	
	RETURN