USE LogisticsOVA

SELECT * FROM [Employees]
SELECT * FROM [Carriers]
SELECT * FROM [Transports]
SELECT * FROM [Routes]
SELECT * FROM [Customers]

SELECT * FROM [Routes]
WHERE routeID=15

SELECT routeID FROM Routes

SELECT COUNT(1) FROM Employees

USE Master
DROP DATABASE LogisticsOVA

DROP TABLE [Transports]

SELECT * FROM Persons p 
JOIN Workers w ON p.id = w.workerID

SELECT * FROM Persons p 
JOIN Customers c ON p.id = c.customerID