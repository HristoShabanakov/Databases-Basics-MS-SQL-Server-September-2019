CREATE DATABASE WMS
USE WMS

GO

CREATE TABLE Clients (
ClientId INT PRIMARY KEY IDENTITY,
FirstName VARCHAR(50) NOT NULL,
LastName VARCHAR(50) NOT NULL,
Phone CHAR(12) NOT NULL -- String length is exactly 12 chars long
)

CREATE TABLE Mechanics (
MechanicId INT PRIMARY KEY IDENTITY,
FirstName VARCHAR(50) NOT NULL,
LastName VARCHAR(50) NOT NULL,
[Address] VARCHAR(255) NOT NULL
)

CREATE TABLE Models (
ModelId INT PRIMARY KEY IDENTITY,
[Name] VARCHAR(50) UNIQUE NOT NULL
)

CREATE TABLE Vendors (
VendorId INT PRIMARY KEY IDENTITY,
[Name] VARCHAR(50) UNIQUE NOT NULL
)

CREATE TABLE Parts (
PartId INT PRIMARY KEY IDENTITY NOT NULL,
SerialNumber VARCHAR(50) UNIQUE NOT NULL,
[Description] VARCHAR(255),
Price DECIMAL(6,2) CHECK (Price > 0) NOT NULL, --Monetary value up to 9999.99, cannot be zero or negative
VendorId INT FOREIGN KEY REFERENCES Vendors(VendorId) NOT NULL,
StockQty INT CHECK (StockQty >= 0) DEFAULT 0 NOT NULL
)

CREATE TABLE Jobs (
JobId INT PRIMARY KEY IDENTITY,
ModelId INT FOREIGN KEY REFERENCES Models(ModelId) NOT NULL,
[Status] VARCHAR(11) NOT NULL
CHECK([Status] IN ('Pending', 'In Progress', 'Finished')) 
DEFAULT 'Pending',
ClientId INT FOREIGN KEY REFERENCES Clients(ClientId) NOT NULL,
MechanicId INT FOREIGN KEY REFERENCES Mechanics(MechanicId),
IssueDate DATE NOT NULL,
FinishDate DATE

--Status allowed values ('Pending', 'In Progress', 'Finished'), Default value 'Pending;.
)

CREATE TABLE PartsNeeded (
JobId INT FOREIGN KEY REFERENCES Jobs(JobId) NOT NULL,
PartId INT FOREIGN KEY REFERENCES Parts(PartId) NOT NULL,
Quantity INT CHECK (Quantity > 0) DEFAULT 1 NOT NULL, --Cannot be zero or negative, Default value is 1
CONSTRAINT PK_PartsNeeded
PRIMARY KEY(JobId, PartId)
)

CREATE TABLE Orders (
OrderId INT PRIMARY KEY IDENTITY,
JobId INT FOREIGN KEY REFERENCES Jobs(JobId) NOT NULL,
IssueDate DATE,
Delivered BIT DEFAULT 0 --Boolean Default value is False
)

CREATE TABLE OrderParts (
OrderId INT FOREIGN KEY REFERENCES Orders(OrderId) NOT NULL,
PartId INT FOREIGN KEY REFERENCES Parts(PartId) NOT NULL,
Quantity INT CHECK (Quantity >= 0) DEFAULT 1 NOT NULL, --Cannot be zero or negative, Default value is 1
CONSTRAINT PK_OrderParts
PRIMARY KEY (OrderId, PartId)
)

