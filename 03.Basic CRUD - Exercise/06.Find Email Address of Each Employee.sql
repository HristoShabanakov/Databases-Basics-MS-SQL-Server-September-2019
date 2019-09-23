  
SELECT e.FirstName + '.' + e.LastName + '@softuni.bg' 
    AS [Full Email Address]
  FROM Employees AS e

  SELECT e.FirstName + ' is ' + e.JobTitle + ' at IHemp Farms'
  AS [Employee Job Title]
  FROM Employees AS e

  