
-- Procedure MembershipInfo_Set
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		AF
-- Create date: 10 June 2009
-- Description:	To insert membership information
-- =============================================
CREATE PROCEDURE [dbo].[MembershipInfo_Set]
(@MemberID bigint
 ,@MemberTypeID smallint
 ,@MembershipStatusID smallint
 ,@LocaleStatusID smallint
 ,@LocaleID int
 ,@GroupID smallint
 ,@CommitteeID smallint
 ,@MembershipDate smalldatetime
 ,@MembershipPlace varchar(200)
 ,@OrientedBy varchar(100)
 ,@OnboardedBy varchar(100)
 ,@PreviousOrganization varchar(200))          
AS
BEGIN
SET NOCOUNT ON;

	IF NOT EXISTS(SELECT MemberID FROM Members WHERE MemberID = rtrim(@MemberID))
	BEGIN
		INSERT INTO Membership
           (MemberID,MemberTypeID,MembershipStatusID,LocaleStatusID,
					  LocaleID,GroupID,CommitteeID,MembershipDate,MembershipPlace,
					  OrientedBy,OnboardedBy,PreviousOrganization)
     VALUES
           (@MemberID,@MemberTypeID,@MembershipStatusID,@LocaleStatusID ,
						@LocaleID,@GroupID,@CommitteeID,@MembershipDate,rtrim(@MembershipPlace),
						rtrim(@OrientedBy),rtrim(@OnboardedBy),rtrim(@PreviousOrganization))
	END
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

