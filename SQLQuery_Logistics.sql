USE LogisticsOVA

SELECT * FROM Workers
SELECT * FROM Customers

SELECT COUNT(1) FROM Workers

USE Master
DROP DATABASE LogisticsOVA

DROP TABLE Workers

SELECT * FROM Persons p 
JOIN Workers w ON p.id = w.workerID

SELECT * FROM Persons p 
JOIN Customers c ON p.id = c.customerID