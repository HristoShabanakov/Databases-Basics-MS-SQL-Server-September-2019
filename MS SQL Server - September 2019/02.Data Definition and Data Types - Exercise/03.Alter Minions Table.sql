ALTER TABLE Minions
ADD CONSTRAINT PK_Id
PRIMARY KEY(Id)

ALTER TABLE Towns
ADD CONSTRAINT PK_TownId
PRIMARY KEY(Id)


ALTER TABLE Minions
ADD TownId INT

ALTER TABLE Minions
ADD CONSTRAINT FK_MinionTownId
FOREIGN KEY (TownId) REFERENCES Towns(Id)