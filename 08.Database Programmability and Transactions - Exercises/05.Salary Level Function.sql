CREATE FUNCTION ufn_GetSalaryLevel(@salary DECIMAL(18,4))
RETURNS VARCHAR(7)
AS
BEGIN
		DECLARE @salaryLevel VARCHAR(7) = CASE
		WHEN @salary < 30000 THEN  'Low'
		WHEN @salary BETWEEN 30000 AND 50000 THEN  'Average'
		ELSE 'High'
		END 
	RETURN @salaryLevel
END

GO

SELECT Salary, dbo.udf_GetSalaryLevel(Salary) AS [Salary Level] FROM Employees