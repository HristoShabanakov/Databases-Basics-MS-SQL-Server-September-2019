SELECT TOP (3)  e.EmployeeID, 
                e.FirstName
           FROM Employees AS e 
     LEFT JOIN  EmployeesProjects AS ep 
             ON ep.EmployeeID = e.EmployeeID
          WHERE ep.EmployeeID IS NULL
	   ORDER BY e.EmployeeID 

--Left Join gets all info from table Employees and check 
--EmployeesProjects for matching data if none it will replace it with NULL