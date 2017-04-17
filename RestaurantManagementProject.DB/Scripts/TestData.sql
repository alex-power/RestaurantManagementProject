INSERT INTO [Users]
(Username,Password,CreationDate,Name,Email)
VALUES ('manager', 'manager', SYSDATETIME(),'Manager Test', 'manager@email.com');
DECLARE @uid INTEGER 
SELECT @uid = SCOPE_IDENTITY();

INSERT INTO [Users_Employee]
(Id, HoursPerWeek, Salary)
VALUES (@uid, 40, 40000)


INSERT INTO [Users_Manager]
(Id)
VALUES (@uid)

INSERT INTO [Users]
(Username, Password, CreationDate, Name, Email)
VALUES ('server', 'server', SYSDATETIME(), 'server test', 'server@email.com')
SELECT @uid = SCOPE_IDENTITY()

INSERT INTO [Users_Employee]
(Id, HoursPerWeek, PayRate)
VALUES (@uid, 40, 10)

INSERT INTO [Users_Server]
(Id, NumTables)
VALUES (@uid, 0)
DECLARE @sid INTEGER
SELECT @sid = @uid

INSERT INTO [Users]
(Username, Password, CreationDate, Name, Email)
VALUES ('kitchen', 'kitchen', SYSDATETIME(), 'kitchen test', 'kitchen@email.com')
SELECT @uid = SCOPE_IDENTITY()

INSERT INTO [Users_Employee]
(Id, HoursPerWeek, PayRate)
VALUES (@uid, 40, 12)
INSERT INTO [Users_Kitchen]
(Id, Role)
VALUES (@uid, 'Chef')

INSERT INTO [Users]
(Username, Password, CreationDate, Name, Email)
VALUES ('customer', 'customer', SYSDATETIME(), 'customer test', 'kitchen@email.com')
SELECT @uid = SCOPE_IDENTITY()
INSERT INTO [Users_Customer]
(Id)
VALUES (@uid)


INSERT INTO [Restaurants]
(Name, Description, Cuisine)
VALUES ('Test Eats', 'Restaurant for good tests', 'Italian')

INSERT INTO [FoodItems]
(Name, Description, Price)
VALUES ('Chicken Test', 'a test chicken', 9.99)

INSERT INTO [FoodItems]
(Name, Description, Price)
VALUES ('Potestos', 'Light and fluffy', 2.00)

INSERT INTO [FoodItems]
(Name, Description, Price)
VALUES ('Eggs and Bacon', 'Well salted', 5.99)

INSERT INTO [FoodItems]
(Name, Description, Price)
VALUES ('Water', '', 0)

INSERT INTO [FoodItems]
(Name, Description, Price)
VALUES ('Coffee', '', 1.50)

INSERT INTO [FoodItems]
(Name, Description, Price)
VALUES ('Soda', '', 1.50)

INSERT INTO [FoodItems]
(Name, Description, Price)
VALUES ('Test Sandwich', 'LOADED with spicy jalepenos', 6.99)

INSERT INTO [FoodItems]
(Name, Description, Price)
VALUES ('Chicken Noodle Soup', 'No shapes, sorry', 4.99)

DECLARE @tid AS INTEGER
INSERT INTO [Tables] ( Seats, Users_Server_Id, TableStatus)
VALUES (4, @sid, 'Open')
SELECT @tid = SCOPE_IDENTITY()

INSERT INTO [Orders]
(TotalPrice, Tip, State, TimeCreated,TimeCompleted, Table_Id)
VALUES (0,0.0,'Ready',SYSDATETIME(), null, @tid)