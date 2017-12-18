
-- Procedure WebObject_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebObject_Set]
	(
		@Id int,
		@Name nvarchar(250),
		@IdentityColumn nvarchar(250),
		@ObjectType nvarchar(250),
		@LastRecordId int,
		@MaxCacheCount int,
		@AccessTypeId int,
		@CacheTypeId int,
		@MaxHistoryCount int,
		@Owner nvarchar(250),
		@Prefix nvarchar(50),
		@DataProviderName nvarchar(250),
		@TypeName nvarchar(250),
		@CacheInterval int,
		@NameColumn nvarchar(250),
		@FriendlyName nvarchar(250),
		@ManagerName nvarchar(500)
	)
AS
	SET NOCOUNT ON
	
	IF(@Id > 0)
		BEGIN
			-- Update
			
			UPDATE       WebObject
			SET                Name = @Name, IdentityColumn = @IdentityColumn, ObjectType = @ObjectType, LastRecordId = @LastRecordId, 
			                         MaxCacheCount = @MaxCacheCount, AccessTypeId = @AccessTypeId, CacheTypeId = @CacheTypeId, 
			                         MaxHistoryCount = @MaxHistoryCount, Owner=@Owner, Prefix=@Prefix, DataProviderName=@DataProviderName,
			                         TypeName = @TypeName, CacheInterval=@CacheInterval, DateModified = GETDATE(), NameColumn=@NameColumn,
			                         FriendlyName=@FriendlyName, ManagerName=@ManagerName
			WHERE        (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert
			
			EXEC @Id = WebObjects_NextId 'WebObject'
			
			INSERT INTO WebObject
			                         (Name, IdentityColumn, ObjectType, LastRecordId, MaxCacheCount, AccessTypeId, CacheTypeId, MaxHistoryCount, Id, 
										Owner, Prefix, DataProviderName, TypeName, CacheInterval, DateModified, NameColumn, FriendlyName, ManagerName)
			VALUES        (@Name,@IdentityColumn,@ObjectType,@LastRecordId,@MaxCacheCount,@AccessTypeId,@CacheTypeId,@MaxHistoryCount,@Id, 
							@Owner, @Prefix, @DataProviderName, @TypeName, @CacheInterval, GETDATE(), @NameColumn, @FriendlyName, @ManagerName)
		END
		
	SELECT @Id
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

