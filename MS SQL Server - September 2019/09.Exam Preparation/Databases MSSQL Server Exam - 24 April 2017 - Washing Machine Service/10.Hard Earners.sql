  SELECT CONCAT(m.FirstName, ' ', m.LastName) AS [Mechanic], 
         COUNT(*) AS[Jobs] FROM Mechanics AS m
    JOIN Jobs AS j
      ON j.MechanicId = m.MechanicId
   WHERE [Status] <> 'Finished'
GROUP BY m.MechanicId, m.FirstName, m.LastName
  HAVING COUNT(*) > 1 -- Filter aggregate data
ORDER BY [Jobs] DESC, m.MechanicId

