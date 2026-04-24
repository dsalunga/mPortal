
-- Procedure WebShare_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [WebShare_Set]
	-- Add the parameters for the stored procedure here
	@Id int =-1,
	@ObjectId int,
	@Recordid int,
	@ShareObjectId int,
	@ShareRecordId int,
	@Allow int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF(@Id > 0)
		BEGIN
			-- Update
			UPDATE  WebShare
			SET     ObjectId = @ObjectId, RecordId = @RecordId, ShareObjectId = @ShareObjectId, 
					ShareRecordId = @ShareRecordId, Allow = @Allow
			WHERE   (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert
			EXEC @Id = WebObject_NextId 'WebShare';

			INSERT INTO WebShare
                         (ObjectId, RecordId, ShareObjectId, ShareRecordId, Allow, Id)
			VALUES	(@ObjectId,@RecordId,@ShareObjectId,@ShareRecordId,@Allow,@Id)
		END

	SELECT @Id;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

