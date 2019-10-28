  SELECT CONCAT(FirstName, ' ', LastName) AS [Avaiable] 
    FROM Mechanics
   WHERE MechanicId NOT IN (SELECT DISTINCT MechanicId FROM JobS
   WHERE MechanicId IS NOT NULL AND [Status] <> 'Finished')
ORDER BY MechanicId