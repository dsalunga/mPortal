
-- Procedure WebAddress_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.WebAddress_Set
	(
		@Id int = -1,
		@AddressLine1 nvarchar(250),
		@AddressLine2 nvarchar(250),
		@CityTown nvarchar(50),
		@StateProvince nvarchar(50),
		@StateProvinceCode int,
		@CountryCode int,
		@ZipCode nvarchar(50),
		@PhoneNumber nvarchar(50),
		@ObjectId int,
		@RecordId int,
		@Tag nvarchar(50),
		@LastUpdated datetime
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update

			UPDATE       WebAddress
			SET                AddressLine1 = @AddressLine1, AddressLine2 = @AddressLine2, CityTown = @CityTown, StateProvince = @StateProvince, 
									 StateProvinceCode = @StateProvinceCode, CountryCode = @CountryCode, ZipCode = @ZipCode, PhoneNumber = @PhoneNumber, 
									 ObjectId = @ObjectId, RecordId = @RecordId, Tag = @Tag, LastUpdated=@LastUpdated
			WHERE        (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert

			EXEC @Id = WebObject_NextId 'WebAddress'

			INSERT INTO WebAddress
			                         (AddressLine1, AddressLine2, CityTown, StateProvince, StateProvinceCode, CountryCode, ZipCode, 
										PhoneNumber, ObjectId, RecordId, Tag, Id, LastUpdated)
			VALUES        (@AddressLine1,@AddressLine2,@CityTown,@StateProvince,@StateProvinceCode,@CountryCode,@ZipCode,@PhoneNumber,
							@ObjectId,@RecordId,@Tag,@Id, @LastUpdated)
		END

	SELECT @Id

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

