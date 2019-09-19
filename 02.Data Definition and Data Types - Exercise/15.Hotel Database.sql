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