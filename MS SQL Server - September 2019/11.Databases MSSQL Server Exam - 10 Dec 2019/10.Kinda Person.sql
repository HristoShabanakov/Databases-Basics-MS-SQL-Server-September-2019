SELECT c.FirstName + ' ' + c.LastName AS [Names], m.Class FROM Clients AS c
LEFT JOIN Orders AS o ON o.ClientId = c.Id
LEFT JOIN Vehicles AS v ON v.Id = o.VehicleId
LEFT JOIN Models AS m ON m.Id = v.ModelId
GROUP BY c.FirstName, c.LastName, m.Class, c.Id
ORDER BY [Names], m.Class, c.Id