CREATE PROC p_Withdraw @AccountId INT, @Amount DECIMAL(15,2) AS
DECLARE @OldBalance DECIMAL(15,2) = (SELECT Balance FROM Accounts WHERE Id = @AccountId)
IF (@OldBalance - @Amount >= 0)
BEGIN
	UPDATE Accounts
	SET Balance -=@Amount
	WHERE Id = @AccountId
END
ELSE
BEGIN
	RAISERROR('Insufficient Funds.', 10, 1)
END

EXEC p_Withdraw 8, 80

SELECT * FROM Accounts