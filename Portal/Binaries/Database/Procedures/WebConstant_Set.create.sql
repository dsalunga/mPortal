
-- Procedure WebConstant_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WebConstant_Set]
	(
		@ConstantId int = -1,
		@Value nvarchar(256),
		@Rank int,
		@Category nvarchar(50),
		@Text nvarchar(256),
		@ObjectId int
	)
AS
	SET NOCOUNT ON
	
	IF(@ConstantId < 1)
		BEGIN
			-- Insert
			EXEC @ConstantId = WebObject_NextId 'WebConstant';
			
			INSERT INTO WebConstant
			                (Value, Rank, Category, Text, ConstantId, ObjectId)
			VALUES        (@Value,@Rank,@Category,@Text,@ConstantId, @ObjectId)
			
			--SELECT IDENT_CURRENT('WebConstants')
		END
	ELSE
		BEGIN
			-- Update
			
			UPDATE    WebConstant
			SET              Value = @Value, Rank = @Rank, Category = @Category, Text = @Text,
					ObjectId=@ObjectId
			WHERE     (ConstantId = @ConstantId)
		END
		
	SELECT @ConstantId;
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

