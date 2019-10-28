SELECT * FROM (SELECT EmployeeId, FirstName, LastName, Salary,
DENSE_RANK () OVER (PARTITION BY Salary ORDER BY EmployeeId) AS Rank
FROM Employees
WHERE Salary BETWEEN 10000 AND 50000) AS temp
WHERE temp.[Rank] = 2
ORDER BY temp.[Salary] DESC