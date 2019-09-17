USE LogisticsOVA

SELECT * FROM [Employees]
SELECT * FROM [Carriers]
SELECT * FROM [Transports]
SELECT * FROM [Routes]
SELECT * FROM [Customers]

SELECT * FROM [Routes]
WHERE routeID like '0-1'

DROP TABLE CityName

CREATE TABLE CityName
(
CityID INT IDENTITY NOT NULL PRIMARY KEY,
CityName NVARCHAR(50)
);

INSERT INTO CityName VALUES
('Нур-Султан'),
('Актау'),
('Актобе'),
('Алматы'),
('Аркалык'),
('Атырау'),
('Жезказган'),
('Караганда'),
('Кызылорда'),
('Кокшетау'),
('Костанай'),
('Павлодар'),
('Петропавловск	'),
('Семей'),
('Талдыкорган'),
('Тараз'),
('Уральск'),
('Усть-Каменогорск'),
('Шымкент')

SELECT * FROM [CityName]

SELECT COUNT(1) FROM Employees

USE Master
DROP DATABASE LogisticsOVA

DROP TABLE [Routes]




SELECT * FROM Persons p 
JOIN Workers w ON p.id = w.workerID

SELECT * FROM Persons p 
JOIN Customers c ON p.id = c.customerID