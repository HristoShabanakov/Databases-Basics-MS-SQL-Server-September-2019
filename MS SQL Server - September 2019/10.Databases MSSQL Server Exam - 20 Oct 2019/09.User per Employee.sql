SELECT e.FirstName, r.Id AS [Report], u.Id AS [User] FROM Employees AS e
JOIN Reports AS r ON e.Id = r.Id
JOIN Users AS u ON u.Id = r.UserId
GROUP BY e.FirstName

--e.FirstName, r.Id AS [Report], u.Id AS [User]