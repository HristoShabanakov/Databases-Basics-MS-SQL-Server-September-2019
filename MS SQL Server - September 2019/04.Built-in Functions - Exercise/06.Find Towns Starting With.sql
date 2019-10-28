SELECT t.TownID, t.[Name] 
FROM Towns AS t
WHERE t.[Name] LIKE '[MKBE]%'
ORDER BY t.[Name]