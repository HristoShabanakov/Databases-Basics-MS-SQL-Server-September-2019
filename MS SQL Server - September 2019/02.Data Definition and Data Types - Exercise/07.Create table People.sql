CREATE TABLE People(
Id INT PRIMARY KEY IDENTITY,
[Name] NVARCHAR(200) NOT NULL,
Picture VARBINARY(MAX),
Height DECIMAL(3,2),
[Weight] DECIMAL (5,2),
Gender CHAR(1) NOT NULL,
Birthdate DATE NOT NULL,
Biography NVARCHAR(MAX)
)

INSERT INTO People ([Name], Picture, Height, [Weight], Gender, Birthdate, Biography)
VALUES 
('John', 2500, 1.8569, 245.00, 'm', '1901-02-25','Unknown'),
('Kate', 11100, 1.50, 48.235, 'f', '1981-08-22','Kate from the bay'),
('Joro', 00025, 1.9569, 75.00, 'm', '1881-01-18','The Life of Joro'),
('Ginka', 27770, 1.70, 52.123, 'f', '1798-06-08','Missing'),
('Bryan', 578900, 1.669, 333.00, 'm', '1971-06-16','Developing')

ALTER TABLE People
ALTER COLUMN Gender CHAR(1) NOT NULL