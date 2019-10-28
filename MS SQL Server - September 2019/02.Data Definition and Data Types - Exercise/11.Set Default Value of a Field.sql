ALTER TABLE Users
ADD DEFAULT GETDATE() FOR LastLoginTime

INSERT INTO Users
(Username, Password, IsDeleted)
VALUES
('May', 'BauBau', 1)