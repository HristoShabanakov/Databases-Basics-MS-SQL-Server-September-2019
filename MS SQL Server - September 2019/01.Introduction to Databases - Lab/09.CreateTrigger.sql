CREATE TRIGGER tr_Transaction ON Accounts
AFTER UPDATE
AS
INSERT INTO Transactions(AccountId, OldBalance, NewBalance, [DateTime])
SELECT inserted.Id, deleted.Balance, inserted.Balance, GETDATE() FROM inserted
JOIN deleted on inserted.Id = deleted.Id

SELECT * FROM Transactions