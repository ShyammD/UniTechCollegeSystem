-- Create Database
CREATE DATABASE unitech_college_db;
USE unitech_college_db;

-- Create Courses Table
CREATE TABLE Courses (
    CourseID INT PRIMARY KEY AUTO_INCREMENT,
    CourseName VARCHAR(255) NOT NULL,
    CourseDescription TEXT
);

-- Select all data from the Courses table
SELECT * FROM Courses;

-- Create Modules Table
CREATE TABLE Modules (
    ModuleID INT PRIMARY KEY AUTO_INCREMENT,
    CourseID INT,
    ModuleCode VARCHAR(50) NOT NULL,
    ModuleTitle VARCHAR(255) NOT NULL,
    ModuleDescription TEXT,
    FOREIGN KEY (CourseID) REFERENCES Courses(CourseID) ON DELETE CASCADE
);

-- Select all data from the Modules table
SELECT * FROM Modules;

-- Create Users Table
CREATE TABLE Users (
    UserID INT PRIMARY KEY AUTO_INCREMENT,
    FullName VARCHAR(255) NOT NULL,
    Email VARCHAR(255) UNIQUE NOT NULL,
    Password VARCHAR(255) NOT NULL,
    FailedLoginAttempts INT DEFAULT 0,
    LastFailedLogin DATETIME NULL
);

-- Select all data from the Users table
SELECT * FROM Users;

-- Create UserModules Table
CREATE TABLE UserModules (
    UserModuleID INT PRIMARY KEY AUTO_INCREMENT,
    UserID INT,
    ModuleID INT,
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE,
    FOREIGN KEY (ModuleID) REFERENCES Modules(ModuleID) ON DELETE CASCADE,
    UNIQUE (UserID, ModuleID)
);

-- Select all data from the UserModules table
SELECT * FROM UserModules;

-- Create Enrollments Table (User registering for Courses)
CREATE TABLE Enrollments (
    EnrollmentID INT PRIMARY KEY AUTO_INCREMENT,
    UserID INT,
    CourseID INT,
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE,
    FOREIGN KEY (CourseID) REFERENCES Courses(CourseID) ON DELETE CASCADE
);

-- Select all data from the Enrollments table
SELECT * FROM Enrollments;


