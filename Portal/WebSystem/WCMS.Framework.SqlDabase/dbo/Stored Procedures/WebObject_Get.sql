CREATE PROCEDURE [dbo].[WebObject_Get]
	(
		@Id int = -1
	)
AS
	SET NOCOUNT ON
	
	SELECT     Id, Name, IdentityColumn, ObjectType, LastRecordId, MaxCacheCount, AccessTypeId, CacheTypeId, MaxHistoryCount, 
				Owner, Prefix, DataProviderName, TypeName, CacheInterval, DateModified, ManagerName, NameColumn, FriendlyName
	FROM         WebObject
	WHERE     (@Id = - 1) OR
	                      (@Id = @Id)
	ORDER BY Name, Id
	
	RETURN