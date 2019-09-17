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
('���-������'),
('�����'),
('������'),
('������'),
('�������'),
('������'),
('���������'),
('���������'),
('���������'),
('��������'),
('��������'),
('��������'),
('�������������	'),
('�����'),
('�����������'),
('�����'),
('�������'),
('����-�����������'),
('�������')

SELECT * FROM [CityName]

SELECT COUNT(1) FROM Employees

USE Master
DROP DATABASE LogisticsOVA

DROP TABLE [Routes]




SELECT * FROM Persons p 
JOIN Workers w ON p.id = w.workerID

SELECT * FROM Persons p 
JOIN Customers c ON p.id = c.customerID