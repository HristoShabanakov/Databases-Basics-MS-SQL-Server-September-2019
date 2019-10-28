CREATE PROC usp_GetHoldersWithBalanceHigherThan @minBalance MONEY
AS
BEGIN
	SELECT ah.FirstName, ah.LastName
	FROM Accounts AS a
	JOIN AccountHolders AS ah
	ON ah.Id = a.AccountHolderId
	GROUP BY ah.FirstName, ah.LastName
	HAVING SUM(a.Balance) > @minBalance
	ORDER BY ah.FirstName, ah.LastName
END