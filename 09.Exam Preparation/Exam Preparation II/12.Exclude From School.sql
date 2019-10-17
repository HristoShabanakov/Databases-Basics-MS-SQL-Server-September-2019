CREATE PROCEDURE usp_ExcludeFromSchool (@studentId INT)
AS
BEGIN
	DECLARE @studentsMatchingIdCount INT = (SELECT COUNT(*) FROM  Students WHERE Id = @studentId)

	IF(@studentsMatchingIdCount = 0)
	BEGIN
	RAISERROR ('This school has no student with the provided id!', 16, 1)
	RETURN 
	END

	DELETE FROM StudentsExams
	WHERE StudentId = @studentId

	DELETE FROM StudentsSubjects
	WHERE StudentId = @studentId

	DELETE FROM StudentsTeachers
	WHERE StudentId = @studentId

	DELETE FROM Students
	WHERE Id = @studentId

END