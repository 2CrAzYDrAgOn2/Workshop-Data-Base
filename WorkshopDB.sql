CREATE DATABASE WorkshopDB;
USE WorkshopDB;

CREATE TABLE Materials (
    MaterialID INT PRIMARY KEY IDENTITY(1,1),
    MaterialName VARCHAR(50)
);

CREATE TABLE Masters (
    MasterID INT PRIMARY KEY IDENTITY(1,1),
    FullName VARCHAR(100),
    Address VARCHAR(200),
    Phone VARCHAR(18),
    DateOfBirth DATE,
    InsuranceNumber VARCHAR(20),
    INN VARCHAR(12)
);

CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    ModelNumber VARCHAR(50),
    ModelName VARCHAR(100),
    ProductType VARCHAR(50),
    Size VARCHAR(20),
    Description VARCHAR(MAX)
);

CREATE TABLE Orders (
    OrderID INT PRIMARY KEY IDENTITY(1,1),
    CustomerName VARCHAR(100),
    OrderDate DATE,
    ExecutionDate DATE,
    Price INT,
    SpecialInstructions VARCHAR(MAX),
    MasterID INT,
    MaterialID INT,
    FOREIGN KEY (MasterID) REFERENCES Masters (MasterID),
    FOREIGN KEY (MaterialID) REFERENCES Materials (MaterialID)
);

CREATE TABLE Registration (
	UserID INT PRIMARY KEY IDENTITY(1,1),
	UserLogin VARCHAR(50),
	UserPassword VARCHAR(50)
);

INSERT INTO Materials (MaterialName)
VALUES
    ('Дерево'),
    ('Металл'),
    ('Пластик'),
    ('Стекло'),
    ('Ткань');

INSERT INTO Masters (FullName, Address, Phone, DateOfBirth, InsuranceNumber, INN)
VALUES
    ('Иванов Иван Иванович', 'ул. Ленина, 123', '+7 (123) 456-78-90', '1980-05-15', '12345-67890-12345', '123456789012'),
    ('Петров Петр Петрович', 'ул. Пушкина, 456', '+7 (987) 654-32-10', '1975-11-20', '54321-09876-54321', '987654321009'),
    ('Сидорова Елена Васильевна', 'пр. Гагарина, 789', '+7 (234) 567-89-01', '1990-03-30', '98765-43210-98765', '234567890109');

INSERT INTO Products (ModelNumber, ModelName, ProductType, Size, Description)
VALUES
    ('12345', 'Стул', 'Мебель', '50x50x80', 'Деревянный стул с подушкой'),
    ('67890', 'Стол', 'Мебель', '120x80x75', 'Стеклянный стол с металлическими ножками'),
    ('54321', 'Лампа', 'Освещение', '30x30x60', 'Столовая лампа с абажуром'),
    ('24680', 'Плед', 'Текстиль', '150x200', 'Мягкий плед из натуральной ткани');

INSERT INTO Orders (CustomerName, OrderDate, ExecutionDate, Price, SpecialInstructions, MasterID, MaterialID)
VALUES
    ('Анна Смирнова', '2023-10-15', '2023-11-05', 5000, 'Изготовить по индивидуальному дизайну', 1, 1),
    ('Максим Козлов', '2023-09-20', '2023-10-10', 7500, 'Покрасить в красный цвет', 2, 2),
    ('Екатерина Иванова', '2023-11-01', '2023-11-20', 3000, 'Без дополнительных инструкций', 3, 4);

INSERT INTO Registration (UserLogin, UserPassword)
VALUES
	('admin', 'admin');

SELECT * FROM Materials;
SELECT * FROM Masters;
SELECT * FROM Products;
SELECT * FROM Orders;
SELECT * FROM Registration;

DROP DATABASE WorkshopDB;