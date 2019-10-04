CREATE TABLE Students(
StudentID INT IDENTITY NOT NULL,
[Name] NVARCHAR(40) NOT NULL

CONSTRAINT PK_Students
PRIMARY KEY (StudentID),

)

CREATE TABLE Exams (
ExamID INT IDENTITY(101, 1) NOT NULL,
[Name] NVARCHAR(40) NOT NULL

CONSTRAINT PK_Exams
PRIMARY KEY (ExamID),

)

CREATE TABLE StudentsExams(
StudentID INT  NOT NULL,
ExamID INT  NOT NULL

CONSTRAINT PK_CompositeStudentIDExamID
PRIMARY KEY (StudentID, ExamID) --Composite Primary Key

CONSTRAINT FK_Students_ExamID
FOREIGN KEY (ExamId)
REFERENCES Exams(ExamID),

CONSTRAINT FK_Students_StudentID
FOREIGN KEY (StudentID)
REFERENCES Students(StudentID)
)

INSERT INTO Students ([Name])
VALUES		('Mila'),
			('Toni'),
			('Ron')

INSERT INTO Exams ([Name])
VALUES		('SpringMVC'),
			('Neo4j'),
			('Oracle 11g')

INSERT INTO StudentsExams (StudentID, ExamID)
VALUES		(1, 101),
			(1, 102),
			(2, 101),
			(3, 103),
			(2, 102),
			(2, 103)