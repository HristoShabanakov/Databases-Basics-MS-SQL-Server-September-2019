CREATE PROC p_Deposit @AccountId INT, @Amount DECIMAL (15,2) AS
UPDATE Accounts
SET Balance += @Amount
WHERE Id = @AccountId

EXEC p_Deposit 8,50

SELECT * FROM	v_ClientAccounts