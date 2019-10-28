SELECT w.DepositGroup,
SUM(DepositAmount) AS TotalSum
FROM WizzardDeposits AS w
WHERE MagicWandCreator = 'Ollivander family'
GROUP BY w.DepositGroup