CREATE TABLE Persons(
PersonID INT IDENTITY NOT NULL,
FirstName NVARCHAR(40) NOT NULL,
Salary DECIMAL(10,2) NOT NULL,
PassportID INT NOT NULL)

INSERT INTO Persons (FirstName, Salary, PassportID)
VALUES  ('Roberto', 43300.00, 102),
		('Tom', 56100.00, 103),
		('Yana', 60200.00, 101)

CREATE TABLE Passports(
PassportID INT IDENTITY(101,1) NOT NULL,
PassportNumber NVARCHAR(20) NOT NULL
)

INSERT INTO Passports (PassportNumber)
VALUES ('N34FG21B'),
		('K65LO4R7'),
		('ZE657QP2')

ALTER TABLE Persons
ADD CONSTRAINT PK_PersonsId
PRIMARY KEY(PersonID)

ALTER TABLE Passports
ADD CONSTRAINT PK_Persons_Passports
PRIMARY KEY (PassportID)

ALTER TABLE Persons
ADD CONSTRAINT FK_Passports_Persons
FOREIGN KEY (PassportID)
REFERENCES Passports(PassportID)