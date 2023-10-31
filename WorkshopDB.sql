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
    ('������'),
    ('������'),
    ('�������'),
    ('������'),
    ('�����');

INSERT INTO Masters (FullName, Address, Phone, DateOfBirth, InsuranceNumber, INN)
VALUES
    ('������ ���� ��������', '��. ������, 123', '+7 (123) 456-78-90', '1980-05-15', '12345-67890-12345', '123456789012'),
    ('������ ���� ��������', '��. �������, 456', '+7 (987) 654-32-10', '1975-11-20', '54321-09876-54321', '987654321009'),
    ('�������� ����� ����������', '��. ��������, 789', '+7 (234) 567-89-01', '1990-03-30', '98765-43210-98765', '234567890109');

INSERT INTO Products (ModelNumber, ModelName, ProductType, Size, Description)
VALUES
    ('12345', '����', '������', '50x50x80', '���������� ���� � ��������'),
    ('67890', '����', '������', '120x80x75', '���������� ���� � �������������� �������'),
    ('54321', '�����', '���������', '30x30x60', '�������� ����� � ��������'),
    ('24680', '����', '��������', '150x200', '������ ���� �� ����������� �����');

INSERT INTO Orders (CustomerName, OrderDate, ExecutionDate, Price, SpecialInstructions, MasterID, MaterialID)
VALUES
    ('���� ��������', '2023-10-15', '2023-11-05', 5000, '���������� �� ��������������� �������', 1, 1),
    ('������ ������', '2023-09-20', '2023-10-10', 7500, '��������� � ������� ����', 2, 2),
    ('��������� �������', '2023-11-01', '2023-11-20', 3000, '��� �������������� ����������', 3, 4);

INSERT INTO Registration (UserLogin, UserPassword)
VALUES
	('admin', 'admin');

SELECT * FROM Materials;
SELECT * FROM Masters;
SELECT * FROM Products;
SELECT * FROM Orders;
SELECT * FROM Registration;

DROP DATABASE WorkshopDB;