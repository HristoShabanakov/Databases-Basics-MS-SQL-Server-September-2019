SELECT TOP(5) k.CountryName, 
              k.[Highest Peak Name], 
			  k.[Highest Peak Elevation], 
			  k.Mountain
FROM
(
      SELECT c.CountryName,
	         ISNULL(p.PeakName, '(no highest peak') AS [Highest Peak Name],
	         ISNULL(p.Elevation, 0) AS [Highest Peak Elevation],
	         ISNULL(m.MountainRange, '(no mountain') AS Mountain,
	         DENSE_RANK () OVER (PARTITION BY c.CountryName ORDER BY p.Elevation DESC) AS ElevationRank
        FROM Countries AS c
   LEFT JOIN MountainsCountries AS mc ON mc.CountryCode = c.CountryCode
   LEFT JOIN Mountains AS m ON m.Id = mc.MountainId
   LEFT JOIN Peaks AS p ON p.MountainId = m.Id) AS k
       WHERE k.ElevationRank = 1
    ORDER BY CountryName, k.[Highest Peak Name] 