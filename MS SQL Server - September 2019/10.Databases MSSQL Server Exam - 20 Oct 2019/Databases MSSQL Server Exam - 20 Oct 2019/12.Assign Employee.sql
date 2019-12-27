CREATE PROCEDURE usp_AssignEmployeeToReport(@EmployeeId INT, @ReportId INT)
AS
BEGIN

DECLARE @employeeDepartment INT = (SELECT e.DepartmentId FROM
Employees AS e
WHERE e.Id = @EmployeeId)

DECLARE @reportDepartment INT = (SELECT c.DepartmentId FROM
Reports AS r
JOIN Categories AS c ON c.Id = r.CategoryId
WHERE r.Id = @ReportId)

IF(@employeeDepartment <> @reportDepartment)
BEGIN
	RAISERROR('Employee doesn''t belong to the appropriate department!', 16, 1)
	RETURN
END

UPDATE Reports
   SET EmployeeId = @EmployeeId
 WHERE Id = @ReportId

END



