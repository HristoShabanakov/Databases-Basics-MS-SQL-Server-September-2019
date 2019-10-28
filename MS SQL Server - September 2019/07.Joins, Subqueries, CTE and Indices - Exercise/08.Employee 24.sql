SELECT e.EmployeeID, e.FirstName, IIF(p.StartDate >= '01.01.2005', NULL, p.[Name]) AS ProjectName
FROM Employees AS e
JOIN EmployeesProjects AS ep ON ep.EmployeeID = e.EmployeeID
JOIN Projects AS p ON  p.ProjectID = ep.ProjectID
WHERE ep.EmployeeID = 24 

--The IIF() function returns a value if a condition is TRUE, or another value if a condition is FALSE.
