CREATE PROCEDURE [GenericListColumn_GetPartition]
	(
		/* @SurveyID int = null, */
		@PartitionId int = -1
		/* @PrePageID int = null */
	)
AS
	SET NOCOUNT ON
	
	/*
	DECLARE @NextPageID int
	
	if(@PrePageID is not null AND @SurveyID is not null)
		begin
			SET @NextPageID = (SELECT TOP 1 PageID FROM SurveyPages WHERE SurveyID=@SurveyID AND Rank > @PrePageID ORDER BY Rank)
		end
	else if(@PageID is not null)
		begin
			SET @NextPageID = @PageID
		end
	else if(@SurveyID is not null)
		begin
			SET @NextPageID = (SELECT TOP 1 PageID FROM SurveyPages WHERE SurveyID=@SurveyID ORDER BY Rank)
		end
	*/
		
	SELECT     Id, ListId, PartitionId, Rank, Label, IsHorizontal, IsRequired
	FROM         GenericListColumn
	WHERE     (PartitionId = @PartitionId)
	ORDER BY Rank;
	RETURN