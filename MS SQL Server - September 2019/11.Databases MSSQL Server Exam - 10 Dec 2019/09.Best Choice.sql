Select m.Manufacturer, m.Model, COUNT(m.[Manufacturer]) AS [TimesOrdered] FROM Models AS m
LEFT JOIN Vehicles AS v ON v.ModelId = m.Id
LEFT JOIN Orders AS o ON o.VehicleId = v.Id 
GROUP BY Manufacturer, m.Model, m.Manufacturer
ORDER BY TimesOrdered DESC, Manufacturer DESC, m.Model ASC