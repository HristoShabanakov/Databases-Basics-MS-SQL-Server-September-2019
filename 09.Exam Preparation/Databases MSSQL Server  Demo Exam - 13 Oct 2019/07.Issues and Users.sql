SELECT i.id, u.Username + ' : ' + i.Title AS IssueAssignee 
FROM Issues AS i 
INNER JOIN Users AS u
ON u.Id = i.AssigneeId
ORDER BY i.Id DESC, IssueAssignee 

