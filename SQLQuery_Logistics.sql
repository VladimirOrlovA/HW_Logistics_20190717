USE LogisticsOVA

SELECT * FROM [Employees]
SELECT * FROM [Customers]
SELECT * FROM [Carriers]
SELECT * FROM [Routes]

SELECT COUNT(1) FROM Employees

USE Master
DROP DATABASE LogisticsOVA

DROP TABLE [Carriers]

SELECT * FROM Persons p 
JOIN Workers w ON p.id = w.workerID

SELECT * FROM Persons p 
JOIN Customers c ON p.id = c.customerID