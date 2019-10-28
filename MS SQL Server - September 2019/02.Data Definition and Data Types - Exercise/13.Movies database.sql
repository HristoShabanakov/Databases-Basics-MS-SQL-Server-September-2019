CREATE DATABASE Movies


CREATE TABLE Directors (
Id INT PRIMARY KEY IDENTITY,
DirectorName NVARCHAR(50) NOT NULL,
Notes NVARCHAR(MAX)
)

CREATE TABLE Genres (
Id INT PRIMARY KEY IDENTITY,
GenreName NVARCHAR(50) NOT NULL,
Notes NVARCHAR(MAX)
)

CREATE TABLE Categories (
Id INT PRIMARY KEY IDENTITY,
CategoryName NVARCHAR(50) NOT NULL,
Notes NVARCHAR(MAX)
)

CREATE TABLE Movies (
Id INT PRIMARY KEY IDENTITY,
Title NVARCHAR(50) NOT NULL,
DirectorId INT,
CopyrightYear INT NOT NULL,
[Length] INT NOT NULL,
GenreId INT,
CategoryId INT,
Rating	DECIMAL(4,1),
Notes NVARCHAR(MAX)
)


ALTER TABLE Movies
ADD CONSTRAINT FK_MoviesCategories
FOREIGN KEY (CategoryId) REFERENCES Categories(Id)

INSERT INTO Directors (DirectorName, Notes)
VALUES
('Christopher Nolan', 'Best known for his cerebral, often nonlinear, storytelling'),
('Night Shyamalan', 'Known for making movies with contemporary supernatural plots'),
('Quentin Tarantino', 'He like to appears in his own movies'),
('Vondie Curtis-Hall', 'Actor and director at the same time'),
('Wes Anderson', 'Likes to shoot with extremely wide-angle anamorphic lenses that exhibit considerable barrel distortion.')

INSERT INTO Genres(GenreName, Notes)
VALUES
('Science fiction','Genre of speculative fiction that has been called the "literature of ideas'),
('Thriller', 'Film with an exciting plot'),
('Comedy', 'Intended to make an audience laugh'),
('Animation', 'the technique of photographing successive drawings'),
('Horror', 'Unsettling films designed to frighten and panic')

INSERT INTO Categories (CategoryName, Notes)
VALUES 
('Popular', 'Mainstream movies'),
('Low budget', NULL),
('Remakes', 'Remake of an existing movie'),
('Directors cut', 'Avaible only on Blu-Ray'),
('Trilogy', 'Set of 3 movies')

INSERT INTO Movies(Title, DirectorId, CopyrightYear, [Length], GenreId, CategoryId, Rating, Notes)
VALUES
('Interstellar', 1, 2015, 2.30, 1, 1, 10, 'One of the best Sci-fi movies'),
('Split', 2, 2019, 2.27, 5, 2, 9, 'Trilogy about Super Heroes'),
('Kill Bill', 3, 2010, 2.00, 4, 2, 9.5, 'If you are looking for action - thats the movie'),
('Gridlock''d', 4, 1997, 1.30, 3, 2, 7.4, 'The last performace of the late Tupac Shakur'),
('Alien', 5, 1992, 1.45, 5, 3, 8.8, 'Classic horror movie')



