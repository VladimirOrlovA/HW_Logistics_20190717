USE LogisticsOVA

SELECT * FROM [Employees]
SELECT * FROM [Carriers]
SELECT * FROM [Transports]
SELECT * FROM [Routes]
SELECT * FROM [Customers]


SELECT COUNT(1) FROM Employees

USE Master
DROP DATABASE LogisticsOVA

DROP TABLE [Routes]

SELECT * FROM Persons p 
JOIN Workers w ON p.id = w.workerID

SELECT * FROM Persons p 
JOIN Customers c ON p.id = c.customerID