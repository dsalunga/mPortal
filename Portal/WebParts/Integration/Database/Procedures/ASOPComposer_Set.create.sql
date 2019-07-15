
-- Procedure MCComposer_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MCComposer_Set]
	@Id int = -1,
		@Name nvarchar(500),
		@Entry nvarchar(MAX),
		@Locale nvarchar(500),
		@Work nvarchar(500),
		@Description nvarchar(MAX),
		@PhotoFile nvarchar(500),
		@NickName nvarchar(500),
		@Active int,
		@CompetitionId int
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update

			UPDATE       MCComposer
			SET         Name = @Name, Entry = @Entry, PhotoFile=@PhotoFile, Locale=@Locale, Work=@Work, Description=@Description,
						NickName=@NickName, Active=@Active, CompetitionId=@CompetitionId
			WHERE        (Id = @Id);
		END
	ELSE
		BEGIN
			-- Insert

			EXEC @Id = WebObject_NextId 'MCComposer';

			INSERT INTO MCComposer
			            (Id,Name, Entry, PhotoFile, Locale, Work, Description, NickName, Active, CompetitionId)
			VALUES      (@Id, @Name,@Entry,@PhotoFile, @Locale, @Work, @Description, @NickName, @Active, @CompetitionId);
		END

	SELECT @Id;

RETURN 0
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

