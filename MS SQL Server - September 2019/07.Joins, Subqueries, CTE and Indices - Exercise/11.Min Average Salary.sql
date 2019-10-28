SELECT MIN(AverageSalary) AS MinAverageSalary FROM 
(SELECT DepartmentID,
	   AVG(Salary) AS AverageSalary
  FROM Employees
GROUP BY DepartmentID) AS AverageSalariesByDepartment