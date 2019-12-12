SELECT t.[Name] AS [TownName], Count(o.[TownId]) AS [OfficesNumber] FROM Towns AS t
JOIN Offices AS o ON o.TownId = T.Id
GROUP BY t.[Name], o.TownId
ORDER BY OfficesNumber DESC, TownName