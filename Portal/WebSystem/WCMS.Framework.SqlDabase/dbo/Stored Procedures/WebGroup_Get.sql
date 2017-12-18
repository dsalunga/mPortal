CREATE PROCEDURE [dbo].[WebGroup_Get]
	(
		@Id int = -1,
		@Name nvarchar(250) = NULL,
		@ParentId int = -2
	)
AS
	SET NOCOUNT ON
	
	SELECT     Id, Name, IsSystem, DateModified, OwnerId, ParentId, JoinApproval, JoinAlert, PageUrl, PageId,
			Description, Managers
	FROM         WebGroup
	WHERE
			(@Id = -1 OR Id = @Id)
		AND	(@Name IS NULL OR Name=@Name)
		AND (@ParentId = -2 OR ParentId=@ParentId)
	ORDER BY
		ParentId, Name
	
	RETURN