CREATE PROCEDURE usp_FindByExtension (@extension VARCHAR(50))
AS
BEGIN
SELECT Id,[Name], CONCAT([Size], 'KB') AS [Size] FROM Files
WHERE CHARINDEX(@extension, [Name]) > 1
ORDER BY Id, [Name], [Size]
END