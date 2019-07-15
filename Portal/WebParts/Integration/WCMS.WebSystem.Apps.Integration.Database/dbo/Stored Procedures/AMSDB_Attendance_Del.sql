CREATE PROCEDURE dbo.ExternalDB_Attendance_Del
	(
		@Id int
	)
AS
	SET NOCOUNT ON

	IF(@Id > 0)
		DELETE FROM [ExternalDB].dbo.Attendances
		WHERE AttendanceID=@Id;

	RETURN