USE Logistics

SELECT * FROM Persons
SELECT * FROM Worker
SELECT * FROM Workers
SELECT * FROM Customers

DROP TABLE Worker

SELECT * FROM Persons p 
JOIN Workers w ON p.id = w.workerID

SELECT * FROM Persons p 
JOIN Customers c ON p.id = c.customerID