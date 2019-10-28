CREATE FUNCTION udf_UserTotalCommits (@username VARCHAR(50))
RETURNS INT
AS
BEGIN 
	RETURN (SELECT COUNT(*) FROM Users AS u
			JOIN Commits AS c
			ON c.ContributorId = u.Id
			WHERE u.Username = @username)
END