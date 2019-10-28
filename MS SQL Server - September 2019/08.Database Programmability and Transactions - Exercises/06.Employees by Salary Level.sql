CREATE PROCEDURE  usp_EmployeesBySalaryLevel(@salaryLevel VARCHAR(7))
AS
BEGIN
	SELECT e.FirstName, e.LastName
	FROM Employees AS e
	WHERE dbo.ufn_GetSalaryLevel(e.Salary) =  @salaryLevel
END

EXEC dbo.usp_EmployeesBySalaryLevel 'High'
