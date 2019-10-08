SELECT e.FirstName, 
       e.LastName,
	   e.HireDate,
	   d.[Name] AS DepartmentName
FROM Employees AS e
INNER JOIN Departments AS d
ON d.DepartmentID = e.DepartmentID
WHERE d.[Name] IN ('Sales', 'Finance')
AND e.HireDate > '01/01/1999'
ORDER BY e.HireDate 