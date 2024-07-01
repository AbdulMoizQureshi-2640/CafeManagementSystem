create database CafeManagementSystem;

-- Creating the Staff table
CREATE TABLE Staff (
    staffID INT identity PRIMARY KEY,
    firstname VARCHAR(255),
    lastname VARCHAR(255),
    email VARCHAR(255),
    password VARCHAR(255),
    phone INT,
    salary FLOAT
);
select * from [admin]
select * from staff
select * from customer
-- Creating the Admin table
CREATE TABLE [Admin] (
    adminID INT identity PRIMARY KEY,
    firstname VARCHAR(255),
    lastname VARCHAR(255),
    email VARCHAR(255),
    password VARCHAR(255),
    phone INT
);

-- Creating the Supplier table
CREATE TABLE Supplier (
    supplierID INT identity PRIMARY KEY,
    CompanyName VARCHAR(255),
    ContactName VARCHAR(255),
    ContactEmail VARCHAR(255),
    ContactPhone INT
);

-- Creating the Customer table
CREATE TABLE Customer (
    customerID INT identity PRIMARY KEY,
    firstname VARCHAR(255),
    lastname VARCHAR(255),
    email VARCHAR(255),
    password VARCHAR(255),
    phone INT,
    loyaltyPoints INT
);


-- Creating the Category table
CREATE TABLE Category (
    categoryID INT IDENTITY PRIMARY KEY,
    name VARCHAR(255)
);


-- Creating the Item table
CREATE TABLE Item (
    itemID INT IDENTITY PRIMARY KEY,
    name VARCHAR(255),
    price FLOAT,
    description VARCHAR(255),
    categoryID INT,
    FOREIGN KEY (categoryID) REFERENCES Category(categoryID)
);

-- Creating the Menu table
CREATE TABLE Menu (
    menuID INT IDENTITY PRIMARY KEY,
    CategoryID INT,
    Description VARCHAR(255),
    FOREIGN KEY (CategoryID) REFERENCES Category(categoryID)
);

-- Creating the Inventory table
CREATE TABLE Inventory (
    inventoryID INT identity PRIMARY KEY,
    ItemID INT,
    quantity INT,
    supplierID INT,
    FOREIGN KEY (supplierID) REFERENCES Supplier(supplierID),
    FOREIGN KEY (ItemID) REFERENCES Item(ItemID)
);

-- Creating the Table table
CREATE TABLE [Table] (
    tableID INT identity PRIMARY KEY,
    Capacity INT,
    Location VARCHAR(255),
	status varchar(255) Default 'Not Reserved'

);


-- Creating the Reservation table
CREATE TABLE Reservation (
    reservationID INT identity PRIMARY KEY,
    customerID INT,
    tableID INT,
    reservationTime DATETIME,
    reservationDate DATETIME,
    FOREIGN KEY (customerID) REFERENCES Customer(customerID),
    FOREIGN KEY (tableID) REFERENCES [Table](tableID)
);

-- Creating the Order table
CREATE TABLE [Order] (
    orderID INT identity PRIMARY KEY,
    customerID INT,
    date DATETIME,
    totalAmount FLOAT,
    status VARCHAR(255) Default 'Pending',
    FOREIGN KEY (customerID) REFERENCES Customer(customerID)
);

-- Creating the OrderDetails table
CREATE TABLE OrderDetails (
    orderDetailsID INT identity PRIMARY KEY,
    orderID INT,
    itemID INT,
    quantity INT,
    subtotal FLOAT,
    FOREIGN KEY (orderID) REFERENCES [Order](orderID),
    FOREIGN KEY (itemID) REFERENCES Item(itemID)
);

-- Creating the CustomerBill table
create table CustomerBill(
    billID INT identity PRIMARY KEY,
    orderID INT,
    amount FLOAT,
    status VARCHAR(255) DEFAULT 'Not Paid',
    FOREIGN KEY (orderID) REFERENCES [Order](orderID)
)

-- Creating the Feedback table
CREATE TABLE Feedback (
    feedbackID INT identity PRIMARY KEY,
    customerID INT,
    orderID INT,
    rating INT,
    comments VARCHAR(255),
    feedbackDate DATETIME,
	status varchar(255) Default 'Under Notice'
    FOREIGN KEY (customerID) REFERENCES Customer(customerID),
    FOREIGN KEY (orderID) REFERENCES [Order](orderID)
);

INSERT INTO Staff (firstname, lastname, email, password, phone, salary)
VALUES
('Ali', 'Hassan', 'alihassan@example.com', 'pass1234', 1234567890, 50000),
('Sana', 'Khan', 'sanakhan@example.com', 'pass1234', 1234567891, 55000),
('Zohaib', 'Ahmed', 'zohaibahmed@example.com', 'pass1234', 1234567892, 52000),
('Ayesha', 'Iqbal', 'ayeshaiqbal@example.com', 'pass1234', 1234567893, 53000),
('Usman', 'Ali', 'usmanali@example.com', 'pass1234', 1234567894, 54000),
('Hania', 'Amir', 'haniaamir@example.com', 'pass1234', 1234567895, 56000),
('Faisal', 'Qureshi', 'faisalqureshi@example.com', 'pass1234', 1234567896, 58000);


INSERT INTO Admin (firstname, lastname, email, password, phone)
VALUES
('Asad', 'Malik', 'asadmalik@example.com', 'pass1234', 1234567897),
('Fatima', 'Zahra', 'fatimazahra@example.com', 'pass1234', 1234567898),
('Kamran', 'Khalid', 'kamrankhalid@example.com', 'pass1234', 1234567899),
('Nimra', 'Khan', 'nimrakhan@example.com', 'pass1234', 1234567800),
('Ammar', 'Yasir', 'ammaryasir@example.com', 'pass1234', 1234567801),
('Saba', 'Qamar', 'sabaqamar@example.com', 'pass1234', 1234567802),
('Junaid', 'Jamshed', 'junaidjamshed@example.com', 'pass1234', 1234567803);


INSERT INTO Supplier (CompanyName, ContactName, ContactEmail, ContactPhone)
VALUES
('Fresh Farms', 'Tariq Shah', 'tariqshah@freshfarms.com', 1234567900),
('Urban Grocers', 'Asif Rehman', 'asifrehman@urbangrocers.com', 1234567901),
('Daily Dairy', 'Farah Khan', 'farahkhan@dailydairy.com', 1234567902),
('Green Orchard', 'Hassan Mir', 'hassanmir@greenorchard.com', 1234567903),
('Spice Route', 'Aliya Hussain', 'aliyahussain@spiceroute.com', 1234567904),
('Bake House', 'Sami Ullah', 'samiullah@bakehouse.com', 1234567905),
('Meat Masters', 'Bilal Javed', 'bilaljaved@meatmasters.com', 1234567906);

INSERT INTO Customer (firstname, lastname, email, password, phone, loyaltyPoints)
VALUES
('Mohammad', 'Ali', 'mohammadali@example.com', 'pass1234', 1234567804, 150),
('Sara', 'Ahmed', 'saraahmed@example.com', 'pass1234', 1234567805, 200),
('Khalid', 'Butt', 'khalidbutt@example.com', 'pass1234', 1234567806, 250),
('Nadia', 'Yousuf', 'nadiayousuf@example.com', 'pass1234', 1234567807, 300),
('Adnan', 'Khan', 'adnankhan@example.com', 'pass1234', 1234567808, 350),
('Zainab', 'Ali', 'zainabali@example.com', 'pass1234', 1234567809, 400),
('Tahir', 'Majeed', 'tahirmajeed@example.com', 'pass1234', 1234567810, 450);


INSERT INTO Category (name)
VALUES
('Fast Food'),
('Desserts'),
('Beverages'),
('Grills'),
('Seafood'),
('Breakfast'),
('Refreshments');


INSERT INTO Item (name, price, description, categoryID)
VALUES
('Chicken Burger', 350, 'Grilled chicken with lettuce, tomato and special sauce', 1),
('Beef Burger', 400, 'Juicy beef patty with cheese and BBQ sauce', 1),
('Lamb Kebab', 500, 'Minced lamb with spices and herbs grilled on skewers', 4),
('Fish Tacos', 450, 'Grilled fish with cabbage slaw and salsa wrapped in corn tortillas', 5),
('Lassi', 150, 'Refreshing traditional yogurt-based drink', 7),
('Pancakes', 300, 'Fluffy pancakes served with honey and berries', 6),
('Falooda', 200, 'Sweet beverage with rose syrup, vermicelli, and milk', 3);


INSERT INTO Menu (CategoryID, Description)
VALUES
(1, 'Delicious and quick fast food options'),
(2, 'Sweet treats and desserts'),
(3, 'Cold and hot beverages'),
(4, 'Tasty and hearty grills'),
(5, 'Fresh seafood dishes'),
(6, 'Start your day right with our breakfast'),
(7, 'Traditional refreshments and drinks');


INSERT INTO Inventory (ItemID, quantity, supplierID)
VALUES
(1, 100, 1),
(2, 100, 1),
(3, 50, 6),
(4, 50, 5),
(5, 200, 2),
(6, 150, 7),
(7, 150, 2);


INSERT INTO [Table] (Capacity, Location)
VALUES
(4, 'Inside corner'),
(4, 'Inside window'),
(6, 'Outside patio'),
(2, 'Bar counter'),
(8, 'Private booth'),
(2, 'Outside balcony'),
(6, 'Family section');


--INSERT INTO Reservation (customerID, tableID, reservationTime, reservationDate)
--VALUES
--(1, 3, '19:00:00', '2023-06-15'),
--(2, 5, '20:00:00', '2023-06-16'),
--(3, 1, '18:00:00', '2023-06-17'),
--(4, 2, '20:30:00', '2023-06-18'),
--(5, 6, '19:00:00', '2023-06-19'),
--(6, 4, '17:00:00', '2023-06-20'),
--(7, 7, '18:30:00', '2023-06-21');



--INSERT INTO [Order] (customerID, date, totalAmount, status)
--VALUES
--(1, '2023-06-15', 350, 'Completed'),
--(2, '2023-06-16', 600, 'Completed'),
--(3, '2023-06-17', 950, 'Completed'),
--(4, '2023-06-18', 700, 'Completed'),
--(5, '2023-06-19', 450, 'Completed'),
--(6, '2023-06-20', 300, 'Completed'),
--(7, '2023-06-21', 200, 'Completed');



--INSERT INTO OrderDetails (orderID, itemID, quantity, subtotal)
--VALUES
--(1, 1, 1, 350),
--(2, 2, 1, 400),
--(2, 7, 1, 200),
--(3, 3, 2, 1000),
--(4, 4, 1, 450),
--(4, 5, 1, 150),
--(5, 6, 1, 300);


--INSERT INTO CustomerBill (orderID, amount, status)
--VALUES
--(1, 350, 'Un Paid'),
--(2, 600, 'Paid'),
--(3, 950, 'Paid'),
--(4, 600, 'Un Paid'),
--(5, 450, 'Un Paid'),
--(6, 300, 'Paid'),
--(7, 200, 'Paid');

--INSERT INTO Feedback (customerID, orderID, rating, comments, feedbackDate)
--VALUES
--(1, 1, 5, 'Excellent food and service!', '2023-06-15'),
--(2, 2, 4, 'Tasty burgers, nice environment', '2023-06-16'),
--(3, 3, 3, 'Good but a bit expensive', '2023-06-17'),
--(4, 4, 4, 'Lovely fish tacos!', '2023-06-18'),
--(5, 5, 5, 'Best breakfast!', '2023-06-19'),
--(6, 6, 4, 'Great Lassi, refreshing!', '2023-06-20'),
--(7, 7, 3, 'Service could be better', '2023-06-21');



--trigger 1
CREATE TRIGGER UpdateBillStatus
ON [Order]
AFTER UPDATE
AS
BEGIN
    IF UPDATE(status)  
    BEGIN
        UPDATE CustomerBill
        SET status = 'Paid'  
        FROM CustomerBill cb
        JOIN inserted i ON cb.orderID = i.orderID
        WHERE i.status = 'Delivered';  
    END
END;





--trigger 1
CREATE TRIGGER UpdateBillStatus
ON [Order]
AFTER UPDATE
AS
BEGIN
    IF UPDATE(status)  
    BEGIN
        UPDATE CustomerBill
        SET status = 'Paid'  
        FROM CustomerBill cb
        JOIN inserted i ON cb.orderID = i.orderID
        WHERE i.status = 'Delivered';  
    END
END;



--trigger 2
CREATE TRIGGER Delete_Item_Trigger
ON Item
INSTEAD OF DELETE
AS
BEGIN
    SET NOCOUNT ON;

    -- Update foreign key columns in OrderDetails to NULL
    UPDATE OrderDetails
    SET itemID = NULL
    FROM OrderDetails od
    INNER JOIN deleted d ON od.itemID = d.itemID;

    -- Update foreign key columns in Inventory to NULL
    UPDATE Inventory
    SET itemID = NULL
    FROM Inventory inv
    INNER JOIN deleted d ON inv.itemID = d.itemID;

    -- Delete the item itself
    DELETE FROM Item
    FROM Item
    INNER JOIN deleted d ON Item.itemID = d.itemID;
END;

--Trigger #3

CREATE TRIGGER trg_IncreaseLoyaltyPoints
ON [Order]
AFTER INSERT
AS
BEGIN
    -- Update loyalty points for each customer who places an order
    UPDATE Customer
    SET loyaltyPoints = ISNULL(loyaltyPoints, 0) + 1
    WHERE customerID IN (SELECT customerID FROM inserted);
END;




