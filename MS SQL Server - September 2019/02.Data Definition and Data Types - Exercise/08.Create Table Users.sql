CREATE TABLE Users(
Id BIGINT IDENTITY NOT NULL,
Username VARCHAR(30) UNIQUE NOT NULL,
Password VARCHAR(26) NOT NULL,
ProfilePicture VARBINARY(MAX),
LastLoginTime DATETIME,
IsDeleted BIT,
CONSTRAINT PK_Users PRIMARY KEY (Id)
)

INSERT INTO Users
VALUES
('Peter', '123456', NULL, NULL, 1),
('Kiro', '123aa6', NULL, NULL, 0),
('Joro', '1tagt6', NULL, NULL, 1),
('Ogi', 'agta451', NULL, NULL, 1),
('Koksun', 'tba456', NULL, NULL, 0)