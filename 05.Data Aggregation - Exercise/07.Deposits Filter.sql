SELECT DepositGroup, SUM(DepositAmount) AS [Total Sum]
FROM WizzardDeposits
WHERE MagicWandCreator = 'Ollivander family'
GROUP BY DepositGroup
HAVING SUM(DepositAmount) < 150000
ORDER BY [Total Sum] DESC