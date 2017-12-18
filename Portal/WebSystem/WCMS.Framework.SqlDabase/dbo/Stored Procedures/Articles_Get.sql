﻿CREATE PROCEDURE [dbo].[Articles_Get]
	(
		@Id int = -1,
		@SiteId int = -2
	)
AS
	SET NOCOUNT ON
	
	SELECT     Id, Title, Image, Description, Date, [Content], Author, SiteId, Active, UserId, DateModified, ModifiedUserId, Tags
	FROM         Articles
	WHERE 
		(@Id = -1 OR Id=@Id)
		AND
		(@SiteId = -2 OR SiteId=@SiteId)
	
	RETURN