CREATE PROCEDURE dbo.WebAddress_Get
	(
		@Id int = -1,
		@Tag nvarchar(50) = NULL,
		@ObjectId int = -1,
		@RecordId int = -1
	)
AS
	SET NOCOUNT ON

	SELECT        Id, AddressLine1, AddressLine2, CityTown, StateProvince, StateProvinceCode, CountryCode, ZipCode, 
					PhoneNumber, ObjectId, RecordId, Tag, LastUpdated
	FROM            WebAddress
	WHERE 
		(@Id = -1 OR Id=@Id)
		AND (@Tag IS NULL OR Tag=@Tag)
		AND (@ObjectId = -1 OR ObjectId=@ObjectId)
		AND (@RecordId = -1 OR RecordId=@RecordId)

	RETURN