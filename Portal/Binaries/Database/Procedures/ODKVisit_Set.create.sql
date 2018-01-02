
-- Procedure ODKVisit_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.ODKVisit_Set
	(
		@Id int =-1,
		@CreatedUserId int,
		@DateCreated datetime,
		@ActualReport ntext,
		@Status ntext,
		@GroupId int,
		@Name nvarchar(250),
		@VisitedUserId int,
		@DateVisited datetime,
		@ActionTaken ntext,
		@ContactNo nvarchar(50),
		@TimesVisited int,
		@Address nvarchar(250),
		@MembershipDate datetime,
		@Tags nvarchar(1000)
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		BEGIN
			-- Update

			UPDATE       ODKVisit
			SET                CreatedUserId = @CreatedUserId, DateCreated = @DateCreated, ActualReport = @ActualReport, Status = @Status, GroupId = @GroupId, 
								Name = @Name, VisitedUserId = @VisitedUserId, DateVisited = @DateVisited, ActionTaken=@ActionTaken, ContactNo=@ContactNo,
								TimesVisited=@TimesVisited, Address=@Address, MembershipDate=@MembershipDate, Tags=@Tags
			WHERE        (Id = @Id)
		END
	ELSE
		BEGIN
			-- Insert

			EXEC @Id = WebObject_NextId 'ODKVisit';

			INSERT INTO ODKVisit
			                         (CreatedUserId, DateCreated, ActualReport, Status, GroupId, Name, VisitedUserId, DateVisited, Id, ActionTaken, ContactNo,
										TimesVisited, Address, MembershipDate, Tags)
			VALUES				(@CreatedUserId,@DateCreated,@ActualReport,@Status,@GroupId,@Name,@VisitedUserId,@DateVisited,@Id, @ActionTaken, @ContactNo,
									@TimesVisited, @Address, @MembershipDate, @Tags)

		END

	SELECT @Id

	RETURN
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

