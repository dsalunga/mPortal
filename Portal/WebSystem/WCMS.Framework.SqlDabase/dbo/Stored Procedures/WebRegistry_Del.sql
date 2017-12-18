CREATE PROCEDURE [dbo].[WebRegistry_Del]
	(
		@RegistryId int = -1,
		@Key nvarchar(255) = null
	)
AS
	SET NOCOUNT ON
	
	if(@RegistryId > 0 or @Key is not null)
		begin
			delete from WebRegistry
				where (@RegistryId = -1 or RegistryId=@RegistryId)
					and (@Key is null or [Key] = @Key)
		end
	
	RETURN