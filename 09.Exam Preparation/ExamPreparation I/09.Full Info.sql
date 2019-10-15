SELECT CONCAT(p.FirstName, ' ', p.LastName) AS [Full Name],
	   pl.[Name] AS [Plane Name],
	   CONCAT(f.Origin, ' - ', f.Destination) AS [Trip],
	   lp.[Type] AS [Luggage Type]
 FROM Passengers AS p
JOIN Tickets AS t
ON t.PassengerId = p.Id
JOIN Flights AS f
ON t.FlightId = f.Id
JOIN Planes AS pl
ON f.PlaneId = pl.Id
JOIN Luggages AS l
ON t.LuggageId = l.Id
JOIN LuggageTypes as lp
ON l.LuggageTypeId = lp.Id
ORDER BY [Full Name], [Plane Name], f.Origin, f.Destination, [Luggage Type]
