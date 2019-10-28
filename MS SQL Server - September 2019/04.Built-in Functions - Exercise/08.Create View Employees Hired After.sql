CREATE VIEW v_EmployeesHiredAfter2000 AS
SELECT FirstName, LastName
FROM Employees
WHERE YEAR(HireDate ) > 2000

SELECT * FROM v_EmployeesHiredAfter2000
