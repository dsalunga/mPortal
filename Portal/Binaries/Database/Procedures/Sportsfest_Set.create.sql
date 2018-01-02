
-- Procedure Sportsfest_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.Sportsfest_Set
	(
		@Id int = -1,
		@MemberId int,
		@Name nvarchar(100),
		@GroupColor nvarchar(50),
		@Age int,
		@Mobile nvarchar(50),
		@EntryDate datetime,
		@Sports nvarchar(50),
		@Locale nvarchar(500),
		@CountryCode int,
		@Suggestion nvarchar(2500),
		@ShirtSize nvarchar(50)
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update

			UPDATE       Sportsfest
			SET                MemberId = @MemberId, Name = @Name, GroupColor = @GroupColor, Age = @Age, Mobile = @Mobile, 
								EntryDate = @EntryDate, Sports=@Sports, Locale = @Locale, CountryCode=@CountryCode,
								Suggestion = @Suggestion, ShirtSize=@ShirtSize
			WHERE        (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert

			EXEC @Id = WebObject_NextId 'Sportsfest';

			INSERT INTO Sportsfest
			                         (MemberId, Name, GroupColor, Age, Mobile, EntryDate, Id, Sports, Locale, CountryCode,
							Suggestion, ShirtSize)
			VALUES        (@MemberId,@Name,@GroupColor,@Age,@Mobile,@EntryDate,@Id, @Sports, @Locale, @CountryCode, @Suggestion,
					@ShirtSize)
		END

	SELECT @Id

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

