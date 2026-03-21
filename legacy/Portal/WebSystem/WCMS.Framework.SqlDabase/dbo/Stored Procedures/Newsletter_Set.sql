CREATE PROCEDURE [dbo].[Newsletter_Set]
	@Id int = -1,
	@Name nvarchar(500),
	@Email nvarchar(50),
	@IPAddress nvarchar(50),
	@SubscribeDate datetime,
	@ObjectId int,
	@RecordId int,
	@SiteId int,
	@Gender int
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update
			UPDATE       Newsletter
			SET         Name = @Name, Email=@Email, IPAddress=@IPAddress, SubscribeDate=@SubscribeDate, ObjectId=@ObjectId,
				RecordId=@RecordId, SiteId=@SiteId, Gender=@Gender
			WHERE        (Id = @Id);
		END
	ELSE
		BEGIN
			-- Insert
			INSERT INTO Newsletter
			            (Name, Email, IPAddress, SubscribeDate, ObjectId, RecordId, SiteId, Gender)
			VALUES      (@Name, @Email, @IPAddress, @SubscribeDate, @ObjectId, @RecordId, @SiteId, @Gender);

			SET @Id = CAST(SCOPE_IDENTITY() AS INT);
		END

	SELECT @Id;
RETURN 0