SELECT CONCAT(e.FirstName, ' ', e.LastName) AS [Employee], 
       d.[Name] AS [Department], 
       c.[Name] AS [Category],
	   r.[Description],
	   FORMAT(r.OpenDate,'dd.MM.yyyy') as[OpenDate],
	   s.Label,
	   u.Name
FROM Reports AS r
JOIN Employees AS e ON e.Id = r.EmployeeId
JOIN Departments AS d ON d.Id = e.DepartmentId
JOIN Categories AS c ON c.DepartmentId = d.Id
JOIN [Status] AS s ON s.Id = r.StatusId
JOIN Users AS u ON u.Id = r.Id
ORDER BY [Employee] ,[Department], [Category], [Description],[OpenDate], u.Name  

--CONCAT(e.FirstName, ' ', e.LastName)

--CONCAT(e.FirstName, ' ', e.LastName) AS [Employee], d.[Name], c.Name FROM

