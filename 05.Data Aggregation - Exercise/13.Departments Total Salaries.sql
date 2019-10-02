SELECT DepartmentID, MAX(Salary) AS TotalSalary
FROM Employees
GROUP BY DepartmentID
