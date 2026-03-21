
-- Procedure Registration_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.Registration_Set
	(
		@Id int = -1,
		@Name nvarchar(100),
		@EntryDate datetime,
		@Locale nvarchar(500),
		@Country nvarchar(250),
		@ExternalId nvarchar(50),
		@Designation nvarchar(250),
		@ArrivalDate datetime,
		@Airline nvarchar(250),
		@FlightNo nvarchar(250),
		@DepartureDate datetime,
		@Address nvarchar(2500),
		@Age int,
		@PlaceType nvarchar(250),
		@Gender nvarchar(50)
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update

			UPDATE       Registration
			SET                Name = @Name, EntryDate = @EntryDate, Locale = @Locale, Country=@Country, ExternalId=@ExternalId,
					Designation=@Designation, ArrivalDate=@ArrivalDate, Airline=@Airline, FlightNo=@FlightNo, 
					DepartureDate=@DepartureDate, Address=@Address, Age=@Age, PlaceType=@PlaceType, Gender=@Gender
			WHERE        (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert

			EXEC @Id = WebObject_NextId 'Registration';

			INSERT INTO Registration
			                         (Name, EntryDate, Id, Locale, Country, ExternalId, Designation, ArrivalDate, Airline,
						FlightNo, DepartureDate, Address, Age, PlaceType, Gender)
			VALUES        (@Name,@EntryDate,@Id, @Locale, @Country, @ExternalId, @Designation, @ArrivalDate, @Airline,
					@FlightNo, @DepartureDate, @Address, @Age, @PlaceType, @Gender)
		END

	SELECT @Id

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

