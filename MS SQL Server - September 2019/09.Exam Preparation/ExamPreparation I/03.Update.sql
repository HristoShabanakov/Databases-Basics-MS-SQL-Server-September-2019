UPDATE Tickets
SET Price *=1.13
WHERE FlightId = (SELECT TOP (1) Id FROM Flights WHERE Destination = 'Carlsbad')