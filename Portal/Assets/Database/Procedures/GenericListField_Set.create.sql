
-- Procedure GenericListField_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [GenericListField_Set]
	(
		@RowId int,
		@ColumnId int,
		@Answer nvarchar(MAX)
	)
AS
	SET NOCOUNT ON
	
	DELETE FROM GenericListField WHERE ColumnId=@ColumnId AND RowId=@RowId
	
	INSERT INTO GenericListField(RowId,ColumnId,Answer) VALUES(@RowId,@ColumnId,@Answer)
								
	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

