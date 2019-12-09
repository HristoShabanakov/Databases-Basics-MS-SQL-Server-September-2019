SELECT u.Username, c.[Name] AS [CategoryName] FROM 
Reports AS r 
JOIN Users AS u ON r.UserId = u.Id
JOIN Categories AS c ON r.CategoryId = c.Id
WHERE DATEPART(MONTH, u.Birthdate) =DATEPART(MONTH, r.OpenDate) AND
DATEPART(DAY, u.Birthdate) = DATEPART(DAY, r.OpenDate)
ORDER BY u.Username, c.[Name]



