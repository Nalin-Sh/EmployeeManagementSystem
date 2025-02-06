# EmployeeManagementSystem

Steps to run the application 

    Step 1:- Create a database 
    Step 2 :- Run all the queries provided below 
    Step 3 :- Configure the connection string in appsettings.json (provide the connection string in 'DefaultConnection)
    Step 4 :- Register a user 
    Step %:- Login with the user and use the application



Database Queries 

create database EmployeeManagementSystem



CREATE TABLE Departments (
    DepartmentID INT IDENTITY(1,1) PRIMARY KEY,
    DepartmentName NVARCHAR(100) NOT NULL UNIQUE
);


CREATE TABLE Employees (
    EmployeeID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    DepartmentID INT NOT NULL FOREIGN KEY REFERENCES Departments(DepartmentID),
    Salary DECIMAL(18,2) NOT NULL,
    DateOfJoining DATETIME NOT NULL,
    IsDeleted BIT DEFAULT 0
);


CREATE PROCEDURE sp_AddEmployee
    @Name NVARCHAR(100), 
    @DepartmentID INT, 
    @Salary DECIMAL(18,2), 
    @DateOfJoining DATETIME
AS
BEGIN
    INSERT INTO Employees (Name, DepartmentID, Salary, DateOfJoining) 
    VALUES (@Name, @DepartmentID, @Salary, @DateOfJoining)
END;


CREATE PROCEDURE sp_GetEmployees
AS
BEGIN
    SELECT e.EmployeeID, e.Name, d.DepartmentName, e.Salary, e.DateOfJoining 
    FROM Employees e
    JOIN Departments d ON e.DepartmentID = d.DepartmentID
    WHERE e.IsDeleted = 0
END;


CREATE PROCEDURE sp_DeleteEmployee
    @EmployeeID INT
AS
BEGIN
    UPDATE Employees SET IsDeleted = 1 WHERE EmployeeID = @EmployeeID
END;


CREATE PROCEDURE sp_GetEmployeeById
    @EmployeeID INT
AS
BEGIN
    SELECT e.EmployeeID, e.Name, d.DepartmentName, e.Salary, e.DateOfJoining 
    FROM Employees e
    JOIN Departments d ON e.DepartmentID = d.DepartmentID
    WHERE e.EmployeeID = @EmployeeID AND e.IsDeleted = 0;
END;

CREATE PROCEDURE sp_UpdateEmployee
    @EmployeeID INT,
    @Name NVARCHAR(100),
    @DepartmentID INT,
    @Salary DECIMAL(18,2),
    @DateOfJoining DATETIME
AS
BEGIN
    UPDATE Employees 
    SET Name = @Name, DepartmentID = @DepartmentID, Salary = @Salary, DateOfJoining = @DateOfJoining
    WHERE EmployeeID = @EmployeeID;
END;


CREATE PROCEDURE sp_AddDepartment
    @DepartmentName NVARCHAR(100),
AS
BEGIN
    INSERT INTO Departments(DepartmentName) 
    VALUES (@DepartmentName)
END;


ALTER PROCEDURE [dbo].[sp_GetDepartments]
AS
BEGIN
    SELECT d.DepartmentName,d.DepartmentID
	from Departments as d 
END;


CREATE TABLE Users (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserName NVARCHAR(256) UNIQUE NOT NULL,
    Email NVARCHAR(256) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(MAX) NOT NULL,
    SecurityStamp NVARCHAR(MAX),
    ConcurrencyStamp NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE Roles (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name NVARCHAR(256) UNIQUE NOT NULL
);

CREATE TABLE UserRoles (
    UserId UNIQUEIDENTIFIER,
    RoleId UNIQUEIDENTIFIER,
    PRIMARY KEY (UserId, RoleId),
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (RoleId) REFERENCES Roles(Id)
);

Insert into Roles (Name)
Values ('Admin');



