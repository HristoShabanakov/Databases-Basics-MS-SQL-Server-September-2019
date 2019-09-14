CREATE PROCEDURE p_AddAccount @ClientId INT, @AccountTypeId INT AS
INSERT INTO Accounts(ClientId, AccountTypeId) VALUES
(@ClientId, @AccountTypeId)

EXEC p_AddAccount 1,2

SELECT * FROM Accounts