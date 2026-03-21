CREATE PROCEDURE dbo.MChapter_Set
	(
		@Id int = -1,
		@Name nvarchar(250),
		@ParentId int,
		@Address nvarchar(1500),
		@Mobile nvarchar(500),
		@Telephone nvarchar(500),
		@Email nvarchar(500),
		@ChapterType int,
		@Latitude float,
		@Longitude float,
		@AccessType int,
		@CountryCode int,
		@ServiceSchedule nvarchar(1500),
		@MoreInfo nvarchar(MAX)
	)
AS
	SET NOCOUNT ON
	
	IF(@Id > 0)
		BEGIN
			-- Update
			
			UPDATE       MChapter
			SET             Id = @Id, Name = @Name, ParentId = @ParentId, [Address] = @Address, Mobile = @Mobile, Telephone = @Telephone, 
			                Email = @Email, ChapterType=@ChapterType, Longitude=@Longitude, Latitude=@Latitude, AccessType=@AccessType,
							CountryCode=@CountryCode, ServiceSchedule=@ServiceSchedule, MoreInfo=@MoreInfo
			WHERE	(Id=@Id)
		END
	ELSE
		BEGIN
			-- Insert
			
			EXEC @Id = WebObject_NextId 'MChapter';
			
			INSERT INTO MChapter
			                (Id, Name, ParentId, [Address], Mobile, Telephone, Email, ChapterType, Longitude, Latitude, AccessType,
							CountryCode, ServiceSchedule, MoreInfo)
			VALUES        (@Id,@Name,@ParentId,@Address,@Mobile,@Telephone,@Email,@ChapterType, @Longitude, @Latitude, @AccessType,
					@CountryCode, @ServiceSchedule, @MoreInfo)
		END
	
	RETURN