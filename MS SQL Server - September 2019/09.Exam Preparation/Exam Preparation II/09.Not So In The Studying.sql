    SELECT CONCAT(s.FirstName, ' ', s.MiddleName + ' ', s.LastName) AS [Full Name] 
      FROM Students AS s
LEFT JOIN StudentsSubjects AS sb
       ON sb.StudentId = s.Id
    WHERE sb.SubjectId IS NULL
 ORDER BY [Full Name]
 -- if the middle name is null concat FirstName and LastName with single space.