CREATE PROC usp_GetEmployeesFromTown @townName VARCHAR(MAX)
AS
BEGIN
	SELECT e.FirstName, e.Lastname
	FROM Employees AS e
	JOIN  Addresses AS a
	ON a.AddressID = e.AddressID
	JOIN Towns AS t
	ON t.TownID = a.TownID
	WHERE t.[Name] = @townName
END