CREATE PROCEDURE usp_MoveVehicle(@vehicleId INT, @officeId INT)
AS
BEGIN

DECLARE @vehicle INT = (SELECT Id FROM Vehicles WHERE Id = @vehicleId)

DECLARE @office INT = (SELECT Id FROM Offices WHERE Id = @officeId)

DECLARE @parkingSpots INT = (SELECT ParkingPlaces FROM Offices)

IF(@parkingSpots > 0)
BEGIN 
   UPDATE Offices
   SET Id = @office
 end
  ELSE 
  RETURN ('Not enough room in this office')
END

SELECT * FROM Offices AS o
JOIN Vehicles AS v ON v.OfficeId = o.Id


