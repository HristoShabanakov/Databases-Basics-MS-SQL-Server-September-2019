CREATE PROCEDURE usp_GetTownsStartingWith @town VARCHAR(20)
AS
 BEGIN
SELECT [Name] 
  FROM Towns
 WHERE LEFT([NAME], LEN(@town)) = @town
   END