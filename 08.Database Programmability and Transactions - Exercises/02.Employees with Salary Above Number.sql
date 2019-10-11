CREATE PROCEDURE usp_GetEmployeesSalaryAboveNumber @salary DECIMAL(18,4)
AS
BEGIN
SELECT e.FirstName, e.LastName
FROM Employees AS e
WHERE e.Salary >= @salary
END