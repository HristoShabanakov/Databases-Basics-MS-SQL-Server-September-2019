  SELECT s.[Name], AVG(sb.Grade) AS [AverageGrade] 
    FROM Subjects AS s
    JOIN StudentsSubjects AS sb
      ON sb.SubjectId = s.Id
GROUP BY s.[Name], s.Id
ORDER BY s.Id

