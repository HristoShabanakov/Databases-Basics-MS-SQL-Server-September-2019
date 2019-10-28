DELETE FROM Tickets
WHERE FlightId = (SELECT TOP (1) Id FROM Flights WHERE Destination = 'Ayn Halagim')

DELETE FROM Flights
WHERE Destination = 'Ayn Halagim'