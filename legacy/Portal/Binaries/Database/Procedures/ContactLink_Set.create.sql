
-- Procedure ContactLink_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ContactLink_Set]
	(
		@RecordId int,
		@ObjectId int,
		
		@ContactID int,
		@Mode int
	)
AS
	SET NOCOUNT ON
	DECLARE @Id int
	
	SET @Id = (SELECT TOP 1 Id FROM ContactLink WHERE ObjectId=@ObjectId AND RecordId=@RecordId)
	
	if(@Id is null)
		begin
			/* INSERT */
			INSERT INTO ContactLink
			                      (RecordId, ObjectId, ContactID, Mode)
			VALUES     (@RecordId,@ObjectId, @ContactID, @Mode)
		end
	else
		begin
			/* UPDATE */
			UPDATE    ContactLink
			SET              ContactID = @ContactID, Mode=@Mode
			WHERE     (Id = @Id)
		end
	
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

