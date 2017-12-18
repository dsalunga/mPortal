
-- Procedure WebParameter_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.WebParameter_Set
	(
		@Id int = -1,
		@ObjectId int,
		@RecordId int,
		@Name nvarchar(250),
		@Value ntext,
		@IsRequired int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update
			UPDATE       WebParameter
			SET                ObjectId = @ObjectId, RecordId = @RecordId, Name = @Name, Value = @Value, IsRequired = @IsRequired
			WHERE        (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert
			EXEC @Id = WebObject_NextId 'WebParameter';

			INSERT INTO WebParameter
			                         (ObjectId, RecordId, Name, Value, IsRequired, Id)
			VALUES        (@ObjectId,@RecordId,@Name,@Value,@IsRequired,@Id)
		END

	SELECT @Id

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

