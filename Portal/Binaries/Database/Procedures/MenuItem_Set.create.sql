
-- Procedure MenuItem_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE MenuItem_Set
	(
		@Id int = -1,
		@MenuId int = -1,
		@ParentId int,
		
		@Text nvarchar(256),
		@NavigateURL nvarchar(256),
		@Target nvarchar(256),
		@IsActive bit,
		@Rank int,
		@PageId int,
		@Type int,
		@CheckPermission int
	)
AS
	SET NOCOUNT ON
	
	if(@Id > 0)
		begin
			/* UPDATE */

			UPDATE    MenuItem
			SET              NavigateURL = @NavigateURL, Text = @Text, Target = @Target, IsActive = @IsActive, 
							Rank = @Rank, PageId = @PageId, Type=@Type, CheckPermission=@CheckPermission,
							ParentId=@ParentId
			WHERE     (Id = @Id)
		end
	else
		begin
			/* INSERT */

			EXEC @Id = WebObject_NextId 'MenuItem';
			
			INSERT INTO MenuItem
			                      (Id, NavigateURL, Text, Target, ParentId, MenuId, IsActive, Rank,
									PageId, Type, CheckPermission)
			VALUES     (@Id, @NavigateURL,@Text,@Target,@ParentId,@MenuId,@IsActive,@Rank,
						@PageId, @Type, @CheckPermission)
		end
	
	SELECT @Id
		
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

