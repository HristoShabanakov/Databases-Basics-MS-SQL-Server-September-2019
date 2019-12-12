CREATE FUNCTION udf_CheckForVehicle(@townName NVARCHAR, @seatsNumber INT)
RETURNS NVARCHAR
AS
BEGIN

DECLARE @seatCheck INT = (SELECT m.Seats FROM Towns AS t
JOIN Offices AS o ON o.TownId = t.Id
JOIN Vehicles AS v ON v.OfficeId = o.Id
JOIN Models AS m ON m.Id = v.ModelId)


DECLARE @townCheck NVARCHAR = (SELECT t.[Name] FROM Towns AS t
JOIN Offices AS o ON o.TownId = t.Id
JOIN Vehicles AS v ON v.OfficeId = o.Id
JOIN Models AS m ON m.Id = v.ModelId)

IF(@townCheck = @townName AND @seatCheck = @seatsNumber)
RETURN ('OfficeName-Model')
ELSE
RETURN ('NO SUCH VEHICLE FOUND')
END

