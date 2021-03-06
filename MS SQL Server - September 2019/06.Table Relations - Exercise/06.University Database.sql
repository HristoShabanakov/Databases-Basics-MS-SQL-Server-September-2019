CREATE DATABASE University

USE University

GO

CREATE TABLE Majors (
MajorID INT PRIMARY KEY NOT NULL,
[Name] NVARCHAR(40) NOT NULL
)

CREATE TABLE Students (
StudentID INT PRIMARY KEY NOT NULL,
StudentNumber INT NOT NULL,
StudentName NVARCHAR(40) NOT NULL,
MajorID INT FOREIGN KEY REFERENCES Majors (MajorID) NOT NULL
)

CREATE TABLE Payments (
PaymentID INT PRIMARY KEY NOT NULL,
PaymentDate DATE NOT NULL,
PaymentAmount DECIMAL(8,2) NOT NULL,
StudentID INT FOREIGN KEY REFERENCES Students (StudentID) NOT NULL
)

CREATE TABLE Subjects (
SubjectID INT PRIMARY KEY NOT NULL,
SubjectName NVARCHAR(40) NOT NULL
)

CREATE TABLE Agenda (
StudentID INT FOREIGN KEY REFERENCES Students (StudentID) NOT NULL,
SubjectID INT FOREIGN KEY REFERENCES Subjects (SubjectID) NOT NULL

CONSTRAINT PK_Composite_StudentID_SubjectID
PRIMARY KEY(StudentID, SubjectID)
)

