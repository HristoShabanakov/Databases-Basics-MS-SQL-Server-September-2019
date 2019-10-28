  SELECT m.ModelId, 
         m.[Name], 
		 CONVERT(varchar(12),AVG(DATEDIFF(DAY, j.IssueDate, J.FinishDate)))+ ' days' AS [Average Service Time]
    FROM Models AS m
    JOIN Jobs AS j ON j.ModelId = m.ModelId
GROUP BY m.ModelId, m.[Name]
ORDER BY AVG(DATEDIFF(DAY, j.IssueDate, J.FinishDate))
 