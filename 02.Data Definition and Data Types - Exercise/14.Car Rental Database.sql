CREATE DATABASE CarRental

USE CarRental

GO

CREATE TABLE Categories(
Id INT IDENTITY NOT NULL,
CategoryName NVARCHAR(50) NOT NULL,
DailyRate DECIMAL(15,2) NOT NULL,
WeeklyRate DECIMAL(15,2) NOT NULL,
MonthlyRate DECIMAL(15,2) NOT NULL,
WeekendRate DECIMAL(15,2) NOT NULL,

CONSTRAINT PK_Categories
PRIMARY KEY(Id)
)

CREATE TABLE Cars (
Id INT IDENTITY,
PlateNumber NVARCHAR(30) NOT NULL,
Manufacturer NVARCHAR(30) NOT NULL,
Model NVARCHAR(30) NOT NULL,
CarYear INT NOT NULL,
CategoryId INT,
Doors INT NOT NULL,
Picture VARBINARY(MAX),
Condition NVARCHAR(MAX),
Available BIT NOT NULL,

CONSTRAINT PK_Cars
PRIMARY KEY(Id),

CONSTRAINT FK_Cars_Categories
FOREIGN KEY (CategoryId)
REFERENCES Categories(Id)
)

CREATE TABLE Employees(
Id INT IDENTITY,
FirstName NVARCHAR(50) NOT NULL,
LastName NVARCHAR(50) NOT NULL,
Title NVARCHAR(25) NOT NULL,
Notes NVARCHAR(MAX),

CONSTRAINT PK_Employees
PRIMARY KEY(Id)
)

CREATE TABLE Customers (
Id INT IDENTITY NOT NULL,
DriverLicenceNumber NVARCHAR(20) NOT NULL,
FullName NVARCHAR(50) NOT NULL,
[Address] NVARCHAR(50) NOT NULL,
City NVARCHAR(30) NOT NULL,
ZIPCode INT NOT NULL,
Notes NVARCHAR(MAX),

CONSTRAINT PK_Customers
PRIMARY KEY(Id)
)

CREATE TABLE RentalOrders(
Id INT IDENTITY,
EmployeeId INT NOT NULL,
CustomerId INT NOT NULL,
CarId INT NOT NULL,
TankLevel NVARCHAR(20) NOT NULL,
KilometrageStart DECIMAL(15,2) NOT NULL,
KilometrageEnd DECIMAL(15,2) NOT NULL,
TotalKilometrage DECIMAL (15,2) NOT NULL,
StartDate DATE NOT NULL,
EndDate DATE NOT NULL,
TotalDays INT NOT NULL,
RateApplied DECIMAL(15,2),
TaxRate DECIMAL(15,2),
OrderStatus NVARCHAR(50),
Notes NVARCHAR(50),

CONSTRAINT PK_RentalOrder
PRIMARY KEY (Id),

CONSTRAINT FK_RentalOrders_Employees
FOREIGN KEY(EmployeeId)
REFERENCES Employees(Id),

CONSTRAINT FK_RentalOrders_Customers
FOREIGN KEY (CustomerId)
REFERENCES Customers(Id)

)

INSERT INTO Categories (CategoryName, DailyRate, WeeklyRate, MonthlyRate, WeekendRate)
VALUES  ('City', 500, 3500, 15000, 700),
		('Sport', 1000, 7000, 30000, 1200),
		('SUV', 1500, 9500, 50000, 2000)

INSERT INTO Cars (PlateNumber, Manufacturer, Model, CarYear, CategoryId, Doors, Picture, Condition, Available)
VALUES  ('GF-178','VW','Polo', 2016, 1, 5, 'As brand new', 1),
		('1-ABC-654', 'BMW', 'M2', 2019, 2, 3, 'New', 1),
		('WG 8976 AG', 'AUDI', 'Q7', 2018, 3, 5, 'Occasion', 1)

INSERT INTO Employees (FirstName, LastName, Title, Notes)
VALUES  ('Kiro', 'Kirov', 'Manager','Best Manager'),
		('Yana', 'Stoykova', 'Sales Representative', 'Saleswoman of the year'),
		('Tosho', 'Toshev', 'Driver', 'Very good driver')

INSERT INTO Customers (DriverLicenceNumber, FullName, [Address], City, ZIPCode, Notes)
VALUES	(123456, 'Ivan Ivanov', 'Green Street 4', 'Barcelona', 1245, 'Silver Member'),
		(789456, 'Devora Ignatov', 'Gold Street 11', 'Sofia', 8795, 'Platinum Member'),
		(456987123, 'Joro Jorev', 'Yellow Street 146', 'Gabrovo', 69325, 'Diamond Member')

INSERT INTO RentalOrders (EmployeeId, CustomerId, CarId, TankLevel, KilometrageStart, KilometrageEnd, 
						TotalKilometrage, StartDate, TotalDays, RateApplied, TaxRate, OrderStatus, EndDate, Notes)
VALUES (1, 1, 1, 25, 43250, 43500, 250, '2018-05-12', 7, 500, 200, 'Completed', '2018-05-14'),
		(2, 2, 2, 75, 10000, 11000, 500, '2000-01-01', 8, 700, 150, 'N/A','2000-02-02'),
		(3, 3, 3, 10, 25000, 30000, 1000, '1996-07-13', 30, 1000, 50, 'Booked', '1996-08-01')