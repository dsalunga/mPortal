
-- Procedure Registration_Get
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.Registration_Get
	(
		@Id int = -1,
		@Name nvarchar(100) = NULL
	)
AS
	SET NOCOUNT ON

	SELECT        Id, Name, EntryDate, Locale, Address, Country, ExternalId, Designation, ArrivalDate, Airline, FlightNo,
			DepartureDate, Address, PlaceType, Age, Gender
	FROM            Registration
	WHERE        (@Id = -1 OR Id = @Id)
		AND (@Name IS NULL OR Name=@Name)

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

