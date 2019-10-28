CREATE PROCEDURE usp_AssignEmployeeToReport(@EmployeeId INT, @ReportId INT)
AS
BEGIN
RETURN
END



SELECT e.Id, c.Id FROM Employees AS e
JOIN Departments AS d ON d.Id = e.Id
JOIN Reports AS r ON r.Id = e.Id
JOIN Categories AS c on c.DepartmentId = r.Id
WHERE e.Id = c.Id
GROUP BY e.Id, c.Id