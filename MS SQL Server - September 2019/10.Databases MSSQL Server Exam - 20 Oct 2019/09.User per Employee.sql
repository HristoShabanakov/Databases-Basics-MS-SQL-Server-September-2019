SELECT e.FirstName + ' ' + e.LastName AS [FullName],
COUNT(r.[UserId]) AS [UsersCount]
FROM Employees AS e
LEFT JOIN Reports AS r ON r.EmployeeId = e.Id
GROUP BY e.FirstName, e.LastName
ORDER BY UsersCount DESC, FullName

