SELECT Username, IpAddress FROM Users
WHERE IpAddress LIKE '___.1_%._%.___'
--_% - getting one or more symbols
ORDER BY Username