CREATE FUNCTION f_CalculateTotalBalance(@ClientId INT) 

RETURNS DECIMAL(15,2) AS

BEGIN
	DECLARE @TotalResult DECIMAL (15,2) = (SELECT SUM(Balance) FROM Accounts WHERE ClientId = 3)

	RETURN @TotalResult
END

GO

SELECT *, dbo.f_CalculateTotalBalance(3) AS TotalBalance FROM Clients
WHERE Id = 3