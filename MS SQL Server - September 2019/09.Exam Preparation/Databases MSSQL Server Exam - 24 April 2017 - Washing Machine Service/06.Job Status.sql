  SELECT [Status], IssueDate 
    FROM Jobs
   WHERE [Status] IN ('In Progress', 'Pending') -- <> 'Finished'
ORDER BY IssueDate, JobId