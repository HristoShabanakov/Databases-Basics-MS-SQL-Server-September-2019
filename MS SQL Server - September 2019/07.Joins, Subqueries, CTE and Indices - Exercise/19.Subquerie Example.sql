SELECT * FROM Employees 
WHERE DepartmentID IN
(SELECT d.DepartmentId
  FROM Departments AS d
WHERE d.[Name] = 'Finance'
OR d.[Name] ='Sales')