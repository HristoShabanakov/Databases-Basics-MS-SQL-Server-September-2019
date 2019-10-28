CREATE DATABASE Hotel

USE Hotel

GO

CREATE TABLE Employees(
Id INT IDENTITY,
FirstName NVARCHAR(50) NOT NULL,
LastName NVARCHAR(50) NOT NULL,
Title NVARCHAR(50) NOT NULL,
Notes NVARCHAR(MAX),

CONSTRAINT PK_Employees
PRIMARY KEY(Id)
)

INSERT INTO Employees (FirstName, LastName, Title, Notes)
VALUES ('Mac', 'Samson', 'Receptionist', 'In training'),
		('Sonya', 'Cage', 'Supervisior', 'F&B'),
		('Sam', 'Smith', 'GM', NULL)

CREATE TABLE Customers (
AccountNumber INT IDENTITY, 
FirstName NVARCHAR(50) NOT NULL, 
LastName NVARCHAR(50) NOT NULL, 
PhoneNumber INT NOT NULL, 
EmergencyName NVARCHAR(50), 
EmergencyNumber INT NOT NULL, 
Notes NVARCHAR(MAX)

CONSTRAINT PK_Customers
PRIMARY KEY(AccountNumber)
)

INSERT INTO Customers (FirstName, LastName, PhoneNumber, EmergencyName, EmergencyNumber, Notes)
VALUES		('Bob', 'Chesh', 12345, 'Ginka', 7845896, 'Platinum'),
			('Cherry', 'Blossom', 784125, NULL, 641339, 'Gold'),
			('Kina', 'Marina', 1425369, 'Jico', 85214793, NULL)


CREATE TABLE RoomStatus (
RoomStatus NVARCHAR(50) NOT NULL,
Notes NVARCHAR(MAX),

CONSTRAINT PK_RoomStatus
PRIMARY KEY (RoomStatus)
)

INSERT INTO RoomStatus (RoomStatus, Notes)
VALUES		('Clean', 'Ready to Use'),
			('Dirty', 'Open the windows to get some air in the room'),
			('Occupied', 'Please bring a bottle of wine and two glasses')

CREATE TABLE RoomTypes (
RoomTypes NVARCHAR(50) NOT NULL,
NOTES NVARCHAR(MAX),

CONSTRAINT PK_RoomTypes
PRIMARY KEY (RoomTypes)
)

INSERT INTO RoomTypes (RoomTypes, NOTES)
VALUES		('Superior', 'High floors only'),
			('Double', NULL),
			('Suite', 'VIP')


CREATE TABLE BedTypes(
BedTypes NVARCHAR(50) NOT NULL,
Notes NVARCHAR(MAX)

CONSTRAINT PK_BedTypes
PRIMARY KEY (BedTypes)
)

INSERT INTO BedTypes (BedTypes, Notes)
VALUES		('Soft', NULL),
			('Family', NULL),
			('Single', 'Extra bed can be provided upon request')


CREATE TABLE Rooms (
RoomNumber INT IDENTITY, 
RoomType NVARCHAR(50) NOT NULL, 
BedType NVARCHAR(50) NOT NULL, 
Rate DECIMAL(15,2) NOT NULL, 
RoomStatus NVARCHAR(50) NOT NULL, 
Notes NVARCHAR(MAX)

CONSTRAINT PK_Rooms
PRIMARY KEY(RoomNumber)

CONSTRAINT FK_RoomType
FOREIGN KEY (RoomType)
REFERENCES RoomTypes(RoomTypes),

CONSTRAINT FK_BedType
FOREIGN KEY (BedType)
REFERENCES BedTypes(BedTypes),

CONSTRAINT FK_RoomStatus
FOREIGN KEY (RoomStatus)
REFERENCES RoomStatus (RoomStatus)
)

INSERT INTO Rooms ( RoomType, BedType, Rate, RoomStatus)
VALUES		('Superior', 'Single', 250, 'Clean'),
			('Superior', 'Soft', 350, 'Clean'),
			('Suite', 'Family', 450, 'Dirty')


CREATE TABLE Payments (
Id INT IDENTITY,
EmployeeId INT, 
PaymentDate DATE NOT NULL,
AccountNumber INT NOT NULL,
FirstDateOccupied DATE NOT NULL, 
LastDateOccupied DATE NOT NULL,
TotalDays INT NOT NULL,
AmountCharged DECIMAL (15,2),
TaxRate DECIMAL(5,2) NOT NULL,
TaxAmount DECIMAL (5,2) NOT NULL,
PaymentTotal DECIMAL (15,2), 
Notes NVARCHAR(MAX)

CONSTRAINT PK_Payments
PRIMARY KEY (Id)

CONSTRAINT FK_Employees_Payments
FOREIGN KEY (EmployeeId)
REFERENCES Employees (Id),

CONSTRAINT FK_Customers_Payments
FOREIGN KEY (AccountNumber)
REFERENCES Customers (AccountNumber)
)

INSERT INTO Payments (EmployeeId, PaymentDate, AccountNumber, FirstDateOccupied, LastDateOccupied, TotalDays, AmountCharged, TaxRate, TaxAmount, PaymentTotal)
VALUES			(1,'2018-11-12', 1, '2005-02-05', '2011-05-18', 70, 1000, 25.20, 50, 1025.20),
				(2,'2017-11-12', 2, '2004-02-05', '2010-05-19', 60, 10000, 78.20, 241, 10215.20),
				(3,'2014-10-10', 3, '2002-06-05', '2012-06-21', 80, 2000, 35.20, 880, 17425.20)

CREATE TABLE Occupancies (
Id INT IDENTITY,
EmployeeId INT, 
DateOccupied DATE,
AccountNumber INT,
RoomNumber INT,
RateApplied DECIMAL (15,2) NOT NULL,
PhoneCharge DECIMAL (15,2), 
Notes NVARCHAR(MAX)

CONSTRAINT PK_Occupancies
PRIMARY KEY (Id),

CONSTRAINT FK_Occupancies_Employees
FOREIGN KEY (EmployeeId)
REFERENCES Employees (Id),

CONSTRAINT FK_Occupancies_Customers
FOREIGN KEY (AccountNumber)
REFERENCES Customers (AccountNumber),

CONSTRAINT FK_Occupancies_Rooms
FOREIGN KEY (RoomNumber)
REFERENCES Rooms (RoomNumber)
)

INSERT INTO Occupancies (EmployeeId,  AccountNumber, RoomNumber, RateApplied)
VALUES	(1, 1, 1, 758.25),
		(2, 2, 2, 668.15),
		(3, 3, 3, 998.86)
