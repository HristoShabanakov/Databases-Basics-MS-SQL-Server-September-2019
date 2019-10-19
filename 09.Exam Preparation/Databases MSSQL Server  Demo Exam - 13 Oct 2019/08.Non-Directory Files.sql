    SELECT f2.Id, 
	       f2.[Name],
		   CONCAT(f2.[Size], 'KB') AS [Size]
      FROM Files AS f
RIGHT JOIN Files AS f2
        ON f2.Id = f.ParentId
     WHERE f.ParentId IS NULL
  ORDER BY f2.Id, f2.[Name], [Size] DESC


