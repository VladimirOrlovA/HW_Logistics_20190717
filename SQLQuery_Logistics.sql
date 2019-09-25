USE LogisticsOVA

SELECT * FROM [Employees]
SELECT * FROM [Carriers]
SELECT * FROM [Transports]
SELECT * FROM [Routes]
SELECT * FROM [Customers]

SELECT * FROM [Routes]
WHERE routeID like '0-1'

SELECT * FROM [Routes]
WHERE routeStart like '������' and routeEnd like '�����'

SELECT * FROM [Routes]
ORDER BY routeStart

DROP TABLE [Routes]


SELECT * FROM Persons p 
JOIN Workers w ON p.id = w.workerID

SELECT * FROM Persons p 
JOIN Customers c ON p.id = c.customerID