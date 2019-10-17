CREATE FUNCTION udf_ExamGradesToUpdate(@studentId INT, @grade DECIMAL(3,2))
RETURNS NVARCHAR(100)
AS 
BEGIN
	DECLARE @studentName NVARCHAR(30) = (SELECT TOP(1) FirstName FROM Students WHERE Id = @studentId)

	IF(@studentName IS NULL)
	BEGIN
	RETURN 'The student with provided id does not exist in the school!'
	END

	IF(@grade > 6.00)
	BEGIN
	RETURN 'Grade cannot be above 6.00!'
	END

	DECLARE @studentGradesCount INT = (SELECT COUNT(Grade) FROM StudentsExams WHERE StudentId = @studentId AND (Grade > @grade AND Grade <= (Grade + 0.50)))

	RETURN CONCAT('You have to update ', @studentGradesCount, ' grades for the student ', @studentName)
END