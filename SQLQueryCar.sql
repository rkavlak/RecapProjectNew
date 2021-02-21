CREATE TABLE Brands(
	BrandId int PRIMARY KEY NOT NULL IDENTITY(1,1),
	BrandName TEXT
)
CREATE TABLE Colors(
	ColorId int PRIMARY KEY NOT NULL IDENTITY(1,1),
	ColorName TEXT
)
CREATE TABLE Cars(
	CarId int PRIMARY KEY NOT NULL IDENTITY(1,1),
	BrandId int, 
	ColorId int,
	ModelYear TEXT,
	DailyPrice decimal,
	Description TEXT,
	FOREIGN KEY (ColorId) REFERENCES Colors(ColorId),
	FOREIGN KEY (BrandId) REFERENCES Brands(BrandId)
)
INSERT INTO Brands(BrandName)
VALUES
('BMW'),
('Audi'),
('Fiat'),
('Renault');
INSERT INTO Colors(ColorName)
VALUES
('White'),
('Black'),
('Red'),
('Blue');
INSERT INTO Cars(BrandId,ColorId,ModelYear,DailyPrice,Description)
VALUES
('1','2','2019','450','Luxury'),
('2','4','2020','500','Sport'),
('4','1','2018','250','Economic'),
('3','3','2017','320','Comfortable');


