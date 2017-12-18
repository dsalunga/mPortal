CREATE PROCEDURE Menu_Get
	(
		@Id int = -1,
		@IsActive int = -1,
		@SiteId int = -2,
		@PageId int = -2
	)
AS
	SET NOCOUNT ON
	
	SELECT     Id, Name, IsActive, DateCreated, UserId, SiteId, PageId, IncludeChildren
			FROM         Menu
			WHERE	(@Id = -1 OR Id=@Id)
					AND (@IsActive=-1 OR IsActive=IsActive)
					AND (@SiteId = -2 OR SiteId=@SiteId)
					AND (@PageId=-2 OR PageId=@PageId)
			ORDER BY Name
			
	RETURN